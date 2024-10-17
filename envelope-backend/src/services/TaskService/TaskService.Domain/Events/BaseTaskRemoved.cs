using MediatR;
using TaskService.Domain.Events.Base;
using TaskService.Domain.Interfaces;

namespace TaskService.Domain.Events;

/// <summary>
/// Событие: Задача удалена
/// </summary>
public class BaseTaskRemoved : BaseTaskEvent, INotification { }