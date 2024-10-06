namespace TaskService.Domain.Events;

/// <summary>
/// Событие: Задача отправлена на проверку
/// </summary>
public class TaskSentToCheck
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Дата отправления на проверку
    /// </summary>
    public DateTime SentToCheckDate { get; set; }
}