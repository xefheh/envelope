using Envelope.Integration.DependencyInjection;
using AuthService.Application.Services;
using AuthService.Application.BackgroundServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddServices(services);
        AddMessageBus(services, configuration);
        AddBackgroundServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<UserService>();
    }

    private static void AddMessageBus(IServiceCollection services, IConfiguration configuration)
    {
        services.AddIntegrationMessageBus(configuration);
    }
    private static void AddBackgroundServices(IServiceCollection services)
    {
        services.AddHostedService<AuthBackgroundService>();
    }
}