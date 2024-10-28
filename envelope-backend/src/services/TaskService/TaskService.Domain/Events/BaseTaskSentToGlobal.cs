using MediatR;
using TaskService.Domain.Events.Base;
using TaskService.Domain.Interfaces;

namespace TaskService.Domain.Events;

/// <summary>
/// Событие: Задача отправлена на проверку
/// </summary>
public class BaseTaskSentToGlobal : BaseTaskEvent, INotification { }