using TaskService.Domain.Interfaces;

namespace TaskService.Application.EventStore;

/// <summary>
/// Интерфейс хранилища событий
/// </summary>
public interface ITaskEventStore
{
    /// <summary>
    /// Добавить событие
    /// </summary>
    /// <param name="taskEvent">Событие задачи</param>
    /// <returns>Пустой Task</returns>
    public Task AddEventAsync(ITaskEvent taskEvent);
    
    /// <summary>
    /// Получить события для агрегата
    /// </summary>
    /// <param name="aggregateId">Id агрегата</param>
    /// <returns>Коллекция событий</returns>
    public Task<ICollection<ITaskEvent>> GetEventsAsync(Guid aggregateId);
    
    /// <summary>
    /// Получить последнее события для агрегата
    /// </summary>
    /// <param name="aggregateId">Id агрегата</param>
    /// <returns>Коллекция событий</returns>
    public Task<ITaskEvent?> GetLastOrDefaultEventAsync(Guid aggregateId);
}