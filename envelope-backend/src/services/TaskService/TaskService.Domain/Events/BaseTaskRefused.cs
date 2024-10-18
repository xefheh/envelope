using MediatR;
using TaskService.Domain.Events.Base;
using TaskService.Domain.Interfaces;

namespace TaskService.Domain.Events;

/// <summary>
/// Событие: задача не прошла в глобальную (отправлена назад в локальное хранилище)
/// </summary>
public class BaseTaskRefused : BaseTaskEvent, INotification { }