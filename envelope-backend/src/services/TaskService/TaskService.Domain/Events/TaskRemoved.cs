using MediatR;
using TaskService.Domain.Interfaces;

namespace TaskService.Domain.Events;

/// <summary>
/// Событие: Задача удалена
/// </summary>
public class TaskRemoved : ITaskEvent, INotification
{
    public Guid Id { get; set; }
    
    public int VersionId { get; set; }
    
    public DateTime EventDate { get; set; }
}