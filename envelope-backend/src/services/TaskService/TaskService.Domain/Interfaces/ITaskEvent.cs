namespace TaskService.Domain.Interfaces;

/// <summary>
/// Интерфейс базового события
/// </summary>
public interface ITaskEvent
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