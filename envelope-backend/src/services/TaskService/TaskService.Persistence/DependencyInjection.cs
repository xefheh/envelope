using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskService.Application.EventStore;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Persistence.Contexts;
using TaskService.Persistence.EventStore;
using TaskService.Persistence.Exceptions;
using TaskService.Persistence.Repositories.ReadOnlyRepositories;
using TaskService.Persistence.Repositories.WriteOnlyRepositories;

namespace TaskService.Persistence;

/// <summary>
/// DI для Persistence слоя
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавить слой Persistence
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="configuration">Конфигурация</param>
    /// <returns>Сервисы с DI</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AddEventStore(services, configuration);
        AddReadOnlyRepositories(services, configuration);
        AddWriteOnlyRepositories(services, configuration);
        return services;
    }

    /// <summary>
    /// Добавить Event Store
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="configuration">Конфигурация</param>
    /// <exception cref="NotFoundConnectionStringException">Исключение - не найдена строка подключения</exception>
    private static void AddEventStore(IServiceCollection services, IConfiguration configuration)
    {
        var eventStoreConnectionString = configuration.GetConnectionString("TaskEventStore");

        if (eventStoreConnectionString == null)
        {
            throw new NotFoundConnectionStringException("TaskEventStore");
        }

        services.AddDbContext<TaskEventStoreContext>(builder => builder.UseNpgsql(eventStoreConnectionString));
        
        services.AddScoped<ITaskEventStore, EfTaskEventStore>();
    }

    /// <summary>
    /// Добавить все репозитории оптимизированные для чтения
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="configuration">Конфигурация</param>
    /// <exception cref="NotFoundConnectionStringException">Исключение - не найдена строка подключения</exception>
    private static void AddReadOnlyRepositories(IServiceCollection services, IConfiguration configuration)
    {
        var taskProjectionDatabaseConnectionString = configuration.GetConnectionString("TaskProjectionDatabase");

        if (taskProjectionDatabaseConnectionString == null)
        {
            throw new NotFoundConnectionStringException("TaskProjectionDatabase");
        }
        
        services.AddDbContext<TaskEventStoreContext>(builder => builder
            .UseNpgsql(taskProjectionDatabaseConnectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        
        services.AddScoped<IGlobalProjectionReadOnlyRepository, EfGlobalProjectionReadOnlyRepository>();
        services.AddScoped<ITaskProjectionReadOnlyRepository, EfTaskProjectionReadOnlyRepository>();
        services.AddScoped<ISentToCheckProjectionReadOnlyRepository, EfSentToCheckProjectionReadOnlyRepository>();
    }
    
    /// <summary>
    /// Добавить все репозитории оптимизированные для записи
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="configuration">Конфигурация</param>
    /// <exception cref="NotFoundConnectionStringException">Исключение - не найдена строка подключения</exception>
    private static void AddWriteOnlyRepositories(IServiceCollection services, IConfiguration configuration)
    {
        var taskProjectionDatabaseConnectionString = configuration.GetConnectionString("TaskProjectionDatabase");

        if (taskProjectionDatabaseConnectionString == null)
        {
            throw new NotFoundConnectionStringException("TaskProjectionDatabase");
        }
        
        services.AddDbContext<TaskEventStoreContext>(builder => builder
            .UseNpgsql(taskProjectionDatabaseConnectionString));
        
        services.AddScoped<IGlobalProjectionWriteOnlyRepository, EfGlobalProjectionWriteOnlyRepository>();
        services.AddScoped<ITaskProjectionWriteOnlyRepository, EfTaskProjectionWriteOnlyRepository>();
        services.AddScoped<ISentToCheckProjectionWriteOnlyRepository, EfSentToCheckProjectionWriteOnlyRepository>();
    }
    
}