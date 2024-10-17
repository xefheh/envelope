namespace TaskService.Domain.Events.Base;

/// <summary>
/// Интерфейс базового события
/// </summary>
public abstract class BaseTaskEvent
{
    /// <summary>
    /// Айди агрегата
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Версия события (порядковый номер)
    /// </summary>
    public int VersionId { get; set; }
    
    /// <summary>
    /// Дата события
    /// </summary>
    public DateTime EventDate { get; set; }
}