namespace TaskService.Domain.Events;

/// <summary>
/// Событие: Задача удалена
/// </summary>
public class TaskRemoved
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Дата удаления
    /// </summary>
    public DateTime RemoveDate { get; set; }
}