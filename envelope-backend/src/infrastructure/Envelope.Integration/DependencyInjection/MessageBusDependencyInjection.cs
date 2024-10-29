using Envelope.Integration.Interfaces;
using Envelope.Integration.MessageBus;
using Envelope.Integration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Envelope.Integration.DependencyInjection;

public static class MessageBusDependencyInjection
{
    public static IServiceCollection AddIntegrationMessageBus(
        this IServiceCollection services,
        IConfiguration configuration,
        string settingsName = "RabbitMqOptions")
    {
        var section = configuration.GetSection(settingsName);

        if (!section.Exists())
        {
            throw new NullReferenceException("RabbitMq settings not found");
        }
        
        services.Configure<RabbitMqOptions>(options =>
        {
            options.Hostname = section["Hostname"]!;
            options.Login = section["Login"]!;
            options.Port = int.Parse(section["Port"]!);
            options.Password = section["Password"]!;
        });

        services.AddSingleton<IMessageBus, RabbitMqMessageBus>();
        
        return services;
    }
}