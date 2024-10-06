namespace TaskService.Domain.Events;

/// <summary>
/// Событие: Задача отправлена на проверку
/// </summary>
public class TaskSentToGlobal
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Id модератор
    /// </summary>
    public Guid Moderator { get; set; }
    
    /// <summary>
    /// Дата отправления на проверку
    /// </summary>
    public DateTime SentToGlobalDate { get; set; }
}