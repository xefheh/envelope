using MediatR;
using TaskService.Domain.Interfaces;

namespace TaskService.Domain.Events;

/// <summary>
/// Событие: задача не прошла в глобальную (отправлена назад в локальное хранилище)
/// </summary>
public class TaskRefused : ITaskEvent, INotification
{
    public Guid Id { get; set; }
    public int VersionId { get; set; }
    public DateTime EventDate { get; set; }
}