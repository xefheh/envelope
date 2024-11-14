using Envelope.Integration.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskService.Application.BackgroundServices;
using TaskService.Application.EventBus.Interfaces;
using TaskService.Application.Handlers.Commands.AddTask;

namespace TaskService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddMediator(services);
        AddEventBus(services);
        AddMessageBus(services, configuration);
        AddBackgroundServices(services);
        return services;
    }

    private static void AddMediator(IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(typeof(AddTaskCommandHandler).Assembly));
    }

    private static void AddEventBus(IServiceCollection services)
    {
        services.AddScoped<IEventBus, EventBus.EventBus>();
    }

    private static void AddMessageBus(IServiceCollection services, IConfiguration configuration)
    {
        services.AddIntegrationMessageBus(configuration);
    }

    private static void AddBackgroundServices(IServiceCollection services)
    {
        services.AddHostedService<TaskBackgroundService>();
    }
}