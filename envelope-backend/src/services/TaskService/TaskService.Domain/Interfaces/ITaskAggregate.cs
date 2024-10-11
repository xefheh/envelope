namespace TaskService.Domain.Interfaces;

/// <summary>
/// Интерфейс агрегата
/// </summary>
public interface IAggregate<in T> where T : class, ITaskEvent
{
    /// <summary>
    /// Id агрегата
    /// </summary>
    public Guid Id { get; set; }

    void Apply(T @event);
}