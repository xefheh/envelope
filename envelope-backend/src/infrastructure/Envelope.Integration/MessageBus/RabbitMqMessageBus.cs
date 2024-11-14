using System.Text;
using System.Text.Json;
using Envelope.Integration.Interfaces;
using Envelope.Integration.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Envelope.Integration.MessageBus;

public class RabbitMqMessageBus : IMessageBus
{
    private readonly ConnectionFactory _factory;

    public RabbitMqMessageBus(IOptions<RabbitMqOptions> options)
    {
        var factory = new ConnectionFactory
        {
            HostName = options.Value.Hostname,
            Port = options.Value.Port,
            UserName = options.Value.Login,
            Password = options.Value.Password,
            DispatchConsumersAsync = true
        };
        _factory = factory;
    }

    public async Task SubscribeAsync<TMessage>(string queueName, Func<TMessage, Task> handleAsync)
    {
        var channel = _factory.CreateConnection().CreateModel();
        DeclareQuery(queueName, channel);
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.Received += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<TMessage>(body);
            if (message != null)
            {
                await handleAsync(message);
            }
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };
        
        await Task.Run(() => channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer));
    }

    public async Task PublishAsync<TMessage>(string queueName, TMessage message)
    {
        var channel = _factory.CreateConnection().CreateModel();
        
        DeclareQuery(queueName, channel);
        
        var strBody = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(strBody);

        await Task.Run(() => channel.BasicPublish(exchange: string.Empty,
            routingKey: queueName,
            body: body));

        channel.Close();
    }

    public async Task<TResponse> SendWithRequestAsync<TMessage, TResponse>(string queueName,
    TMessage message,
    int timeoutInMs = 30000)
{
    var channel = _factory.CreateConnection().CreateModel();
    
    DeclareQuery(queueName, channel);
    
    var correlationId = Guid.NewGuid().ToString();
    var props = channel.CreateBasicProperties();
    
    var replyQueueName = $"{queueName}_reply";
    
    DeclareQuery(replyQueueName, channel);
    
    props.CorrelationId = correlationId;
    props.ReplyTo = replyQueueName;
    
    var strBody = JsonSerializer.Serialize(message);
    var body = Encoding.UTF8.GetBytes(strBody);

    channel.BasicPublish(exchange: string.Empty,
        routingKey: queueName,
        body: body,
        basicProperties: props);
    
    var tcs = new TaskCompletionSource<TResponse>();
    var consumer = new AsyncEventingBasicConsumer(channel);

    consumer.Received += async (_, ea) =>
    {
        if (ea.BasicProperties.CorrelationId == correlationId)
        {
            var responseBody = ea.Body.ToArray();
            var response = JsonSerializer.Deserialize<TResponse>(responseBody);
            tcs.SetResult(response!);
            await Task.Run(() => channel.BasicAck(ea.DeliveryTag, false));
        }
    };

    channel.BasicConsume(queue: replyQueueName, autoAck: false, consumer: consumer);
    
    using var cancellationTokenSource = new CancellationTokenSource(timeoutInMs);
    cancellationTokenSource.Token.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);
    var task =  await tcs.Task;
    channel.Close();
    return task;
}

public async Task SubscribeResponseAsync<TMessage, TResponse>(string queueName, Func<TMessage, Task<TResponse>> handleAsync)
{
    var channel = _factory.CreateConnection().CreateModel();
    
    DeclareQuery(queueName, channel);
    var consumer = new AsyncEventingBasicConsumer(channel);
    
    consumer.Received += async (_, ea) =>
    {
        var requestBody = ea.Body.ToArray();
        var request = JsonSerializer.Deserialize<TMessage>(requestBody);
        
        var replyProps = channel.CreateBasicProperties();
        replyProps.CorrelationId = ea.BasicProperties.CorrelationId;
        replyProps.ReplyTo = ea.BasicProperties.ReplyTo;

        var response = await handleAsync(request!);
        var responseStr = JsonSerializer.Serialize(response);
        var responseBytes = Encoding.UTF8.GetBytes(responseStr);
        
        await Task.Run(() => channel.BasicPublish(exchange: string.Empty,
            routingKey: ea.BasicProperties.ReplyTo,
            body: responseBytes,
            basicProperties: replyProps));
        
        await Task.Run(() => channel.BasicAck(ea.DeliveryTag, false));
    };
    
    await Task.Run(() => channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer));
}


    private static void DeclareQuery(string queueName, IModel channel)
    {
        channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }
}