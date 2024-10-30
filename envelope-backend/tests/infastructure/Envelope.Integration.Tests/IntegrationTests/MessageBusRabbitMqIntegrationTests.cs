using Envelope.Integration.Tests.TestFixtures;
using Xunit.Abstractions;

namespace Envelope.Integration.Tests.IntegrationTests;

[Collection("IntegrationTests")]
public class MessageBusRabbitMqIntegrationTests : IClassFixture<RabbitMqMessageBusFixture>
{
    private readonly RabbitMqMessageBusFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;

    public MessageBusRabbitMqIntegrationTests(RabbitMqMessageBusFixture fixture, ITestOutputHelper testOutputHelper)
    {
        _fixture = fixture;
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task MessageBus_Subscribe_AndPublishOneMessage_Test()
    {
        const string queueName = "test_queue1";
        var requestMessage = new Message { Text = "This is a test message" };
        
        Message? receivedMessage = null;

        await _fixture.ConsumerMessageBus!.SubscribeAsync<Message>(queueName, message =>
        {
            receivedMessage = message;
            _testOutputHelper.WriteLine($"Received message: {receivedMessage}");
            return Task.CompletedTask;
        });

        await _fixture.ProducerMessageBus!.PublishAsync(queueName, requestMessage);
        
        await Task.Delay(1000);

        Assert.NotNull(receivedMessage);
        Assert.Equal(requestMessage.Text, receivedMessage.Text);
    }
    
    [Fact]
    public async Task MessageBus_Subscribe_AndPublishSomeMessage_Test()
    {
        const string queueName = "test_queue2";
        
        List<Message> expectedCollection =
        [
            new() { Text = "This is a test message #1" },
            new() { Text = "This is a test message #2" },
            new() { Text = "This is a test message #3" }
        ];

        List<Message> receivedMessages = [];

        await _fixture.ConsumerMessageBus!.SubscribeAsync<Message>(queueName, message =>
        {
            receivedMessages.Add(message);
            _testOutputHelper.WriteLine($"Received message: {message}");
            return Task.CompletedTask;
        });

        foreach (var message in expectedCollection)
        {
            await _fixture.ProducerMessageBus!.PublishAsync(queueName, message);
        }

        await Task.Delay(1000);

        Assert.Equal(expectedCollection, receivedMessages);
    }
    
    [Fact]
    public async Task MessageBus_Request_Response_Test()
    {
        const string queueName = "test_queue3";
        var requestMessage = new Message { Text = "This is a test message" };

        await _fixture.ConsumerMessageBus!.SubscribeResponseAsync<Message, string>(queueName, message =>
        {
            _testOutputHelper.WriteLine($"Received message: {message}");
            return Task.FromResult(new string(message.Text.Reverse().ToArray()));
        });

        var response = await _fixture.ProducerMessageBus!.SendWithRequestAsync<Message, string>(queueName, requestMessage);

        await Task.Delay(1000);
        
        _testOutputHelper.WriteLine($"Received message: {response}");

        Assert.Equal(new string(requestMessage.Text.Reverse().ToArray()), response);
    }

    private class Message
    {
        public string Text { get; init; } = null!;

        public override string ToString() => Text;

        public override int GetHashCode() => Text.GetHashCode();
        
        public override bool Equals(object? obj) => Text == obj?.ToString();
    }
}