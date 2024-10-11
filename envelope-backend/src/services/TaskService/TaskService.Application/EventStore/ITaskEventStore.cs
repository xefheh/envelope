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
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пустой Task</returns>
    public Task AddEventAsync(ITaskEvent taskEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Получить события для агрегата
    /// </summary>
    /// <param name="aggregateId">Id агрегата</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция событий</returns>
    public Task<ICollection<ITaskEvent>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить последнее события для агрегата
    /// </summary>
    /// <param name="aggregateId">Id агрегата</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция событий</returns>
    public Task<ITaskEvent?> GetLastOrDefaultEventAsync(Guid aggregateId, CancellationToken cancellationToken);
}