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
    private readonly IModel _channel;

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
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    public async Task SubscribeAsync<TMessage>(string queueName, Func<TMessage, Task> handleAsync)
    {
        DeclareQuery(queueName);
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<TMessage>(body);
            if (message != null)
            {
                await handleAsync(message);
            }
            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };
        
        await Task.Run(() => _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer));
    }

    public async Task PublishAsync<TMessage>(string queueName, TMessage message)
    {
        DeclareQuery(queueName);
        
        var strBody = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(strBody);

        await Task.Run(() => _channel.BasicPublish(exchange: string.Empty,
            routingKey: queueName,
            body: body));
    }

    public async Task<TResponse> SendWithRequestAsync<TMessage, TResponse>(string queueName,
    TMessage message,
    int timeoutInMs = 30000)
{
    DeclareQuery(queueName);
    
    var correlationId = Guid.NewGuid().ToString();
    var props = _channel.CreateBasicProperties();
    
    var replyQueueName = $"{queueName}_reply";
    
    DeclareQuery(replyQueueName);
    
    props.CorrelationId = correlationId;
    props.ReplyTo = replyQueueName;
    
    var strBody = JsonSerializer.Serialize(message);
    var body = Encoding.UTF8.GetBytes(strBody);

    _channel.BasicPublish(exchange: string.Empty,
        routingKey: queueName,
        body: body,
        basicProperties: props);
    
    var tcs = new TaskCompletionSource<TResponse>();
    var consumer = new AsyncEventingBasicConsumer(_channel);

    consumer.Received += async (_, ea) =>
    {
        if (ea.BasicProperties.CorrelationId == correlationId)
        {
            var responseBody = ea.Body.ToArray();
            var response = JsonSerializer.Deserialize<TResponse>(responseBody);
            tcs.SetResult(response!);
            await Task.Run(() => _channel.BasicAck(ea.DeliveryTag, false));
        }
    };

    _channel.BasicConsume(queue: replyQueueName, autoAck: false, consumer: consumer);
    
    using var cancellationTokenSource = new CancellationTokenSource(timeoutInMs);
    cancellationTokenSource.Token.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);
    return await tcs.Task;
}

public async Task SubscribeResponseAsync<TMessage, TResponse>(string queueName, Func<TMessage, Task<TResponse>> handleAsync)
{
    DeclareQuery(queueName);
    var consumer = new AsyncEventingBasicConsumer(_channel);
    
    consumer.Received += async (_, ea) =>
    {
        var requestBody = ea.Body.ToArray();
        var request = JsonSerializer.Deserialize<TMessage>(requestBody);
        
        var replyProps = _channel.CreateBasicProperties();
        replyProps.CorrelationId = ea.BasicProperties.CorrelationId;
        replyProps.ReplyTo = ea.BasicProperties.ReplyTo;

        var response = await handleAsync(request!);
        var responseStr = JsonSerializer.Serialize(response);
        var responseBytes = Encoding.UTF8.GetBytes(responseStr);
        
        await Task.Run(() => _channel.BasicPublish(exchange: string.Empty,
            routingKey: ea.BasicProperties.ReplyTo,
            body: responseBytes,
            basicProperties: replyProps));
        
        await Task.Run(() => _channel.BasicAck(ea.DeliveryTag, false));
    };
    
    await Task.Run(() => _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer));
}


    private void DeclareQuery(string queueName)
    {
        _channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }
}