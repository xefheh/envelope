using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskService.Application.EventBus;
using TaskService.Application.EventBus.Interfaces;
using TaskService.Application.EventStore;
using TaskService.Application.Handlers.Commands.AddTask;
using TaskService.Application.Repositories;

namespace TaskService.Tests.TaskServiceApplication.Tests.Infrastructure;

public class MediatorFactory
{
    public static IMediator Create()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IGlobalProjectionRepository, MockGlobalTaskRepository>();
        services.AddSingleton<ITaskEventStore, MockTaskEventStore>();
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(AddTaskCommandHandler).Assembly));
        
        return services.BuildServiceProvider().GetService<IMediator>()!;
    }
    
    public static (IMediator mediator, ITaskEventStore eventStore) CreateWithStore()
    {
        var services = new ServiceCollection();
        
        services.AddScoped<ITaskEventStore, MockTaskEventStore>();
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(AddTaskCommandHandler).Assembly));

        var provider = services.BuildServiceProvider();
        
        return (provider.GetService<IMediator>()!, 
            provider.GetService<ITaskEventStore>()!);
    }
    
    public static (IMediator mediator,
        ITaskEventStore eventStore,
        IGlobalProjectionRepository globalProjectionRepository) CreateWithStoreAndRepository()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IGlobalProjectionRepository, MockGlobalTaskRepository>();
        services.AddSingleton<IEventBus, EventBus>();
        services.AddScoped<ITaskEventStore, MockTaskEventStore>();
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(AddTaskCommandHandler).Assembly));

        var provider = services.BuildServiceProvider();
        
        return (provider.GetService<IMediator>()!, 
            provider.GetService<ITaskEventStore>()!,
            provider.GetService<IGlobalProjectionRepository>()!);
    }
}