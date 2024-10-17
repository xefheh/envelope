using TaskService.Domain.Events.Base;
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
    /// <param name="baseTaskEvent">Событие задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пустой Task</returns>
    public Task AddEventAsync(BaseTaskEvent baseTaskEvent, CancellationToken cancellationToken);

    /// <summary>
    /// Получить события для агрегата
    /// </summary>
    /// <param name="aggregateId">Id агрегата</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция событий</returns>
    public Task<ICollection<BaseTaskEvent>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить последнее события для агрегата
    /// </summary>
    /// <param name="aggregateId">Id агрегата</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция событий</returns>
    public Task<BaseTaskEvent?> GetLastOrDefaultEventAsync(Guid aggregateId, CancellationToken cancellationToken);
}