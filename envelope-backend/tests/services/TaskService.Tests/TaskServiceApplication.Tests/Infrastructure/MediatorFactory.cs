using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskService.Application.EventBus;
using TaskService.Application.EventBus.Interfaces;
using TaskService.Application.EventStore;
using TaskService.Application.Handlers.Commands.AddTask;
using TaskService.Application.Repositories;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Tests.TaskServiceApplication.Tests.Infrastructure.EventStore;
using TaskService.Tests.TaskServiceApplication.Tests.Infrastructure.Repositories;

namespace TaskService.Tests.TaskServiceApplication.Tests.Infrastructure;

/// <summary>
/// Фабрика медиатора (для тестов)
/// </summary>
public class MediatorFactory
{
    /// <summary>
    /// Создать медиатор
    /// </summary>
    /// <returns>Медиатор</returns>
    public static IMediator Create()
    {
        var services = new ServiceCollection();

        AddCommonTestDepends(services);
        
        return services.BuildServiceProvider().GetService<IMediator>()!;
    }
    
    /// <summary>
    /// Создать
    /// </summary>
    /// <returns>Медиатор, Хранилище событий</returns>
    public static (IMediator mediator, ITaskEventStore eventStore) CreateWithStore()
    {
        var services = new ServiceCollection();

        AddCommonTestDepends(services);

        var provider = services.BuildServiceProvider();
        
        return (provider.GetService<IMediator>()!, 
            provider.GetService<ITaskEventStore>()!);
    }
    
    /// <summary>
    /// Создать
    /// </summary>
    /// <returns>Медиатор, хранилище событий, список всех проекций</returns>
    public static (IMediator mediator, ITaskEventStore eventStore, CommonListStorage repository) CreateWithStoreAndRepository()
    {
        var services = new ServiceCollection();

        AddCommonTestDepends(services);

        var provider = services.BuildServiceProvider();
        
        return (provider.GetService<IMediator>()!, 
            provider.GetService<ITaskEventStore>()!,
            provider.GetService<CommonListStorage>()!);
    }

    private static void AddCommonTestDepends(IServiceCollection services)
    {
        services.AddSingleton<IEventBus, EventBus>();
        services.AddSingleton<ITaskEventStore, MockTaskEventStore>();
        services.AddSingleton<CommonListStorage>();
        services.AddSingleton<IGlobalProjectionReadOnlyRepository, MockCommonTaskRepository>();
        services.AddSingleton<IGlobalProjectionWriteOnlyRepository, MockCommonTaskRepository>();
        services.AddSingleton<ISentToCheckProjectionReadOnlyRepository, MockCommonTaskRepository>();
        services.AddSingleton<ISentToCheckProjectionWriteOnlyRepository, MockCommonTaskRepository>();
        services.AddSingleton<ITaskProjectionReadOnlyRepository, MockCommonTaskRepository>();
        services.AddSingleton<ITaskProjectionWriteOnlyRepository, MockCommonTaskRepository>();
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(AddTaskCommandHandler).Assembly));
    }
}