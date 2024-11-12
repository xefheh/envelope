using Envelope.Common.Enums;
using MediatR;
using TaskService.Domain.Events.Base;

namespace TaskService.Domain.Events;

/// <summary>
/// Событие: задача создана
/// </summary>
public class BaseTaskCreated : BaseTaskEvent, INotification
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Описание (содержание)
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Ответ
    /// </summary>
    public string Answer { get; set; } = null!;
    
    /// <summary>
    /// Сложность
    /// </summary>
    public Difficult Difficult { get; set; }
    
    /// <summary>
    /// Время выполнения (в секундах)
    /// </summary>
    public int? ExecutionTime { get; set; }
    
    /// <summary>
    /// Id автора задачи
    /// </summary>
    public Guid Author { get; set; }
}