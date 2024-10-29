using Envelope.Integration.MessageBus;
using Envelope.Integration.Options;
using Testcontainers.RabbitMq;

namespace Envelope.Integration.Tests.TestFixtures;

public class RabbitMqMessageBusFixture : IAsyncLifetime
{
    private RabbitMqContainer? _rabbitMqContainer;
    
    public RabbitMqMessageBus? ProducerMessageBus { get; private set; }
    public RabbitMqMessageBus? ConsumerMessageBus { get; private set; }
    
    public async Task InitializeAsync()
    {
        _rabbitMqContainer = new RabbitMqBuilder()
            .WithExposedPort(15672)
            .WithExposedPort(5672)
            .WithPortBinding("15672", "15672")
            .WithPortBinding("5672", "5672")
            .WithUsername("admin")
            .WithPassword("admin")
            .Build();
        
        await _rabbitMqContainer.StartAsync();
        
        _rabbitMqContainer.GetConnectionString();
        
        var options = Microsoft.Extensions.Options.Options.Create(new RabbitMqOptions
        {
            Hostname = "localhost",
            Port = 5672,
            Login = "admin",
            Password = "admin",
        });
        
        ProducerMessageBus = new RabbitMqMessageBus(options);
        ConsumerMessageBus = new RabbitMqMessageBus(options);
    }

    public async Task DisposeAsync()
    {
        await _rabbitMqContainer!.StopAsync();
    }
}