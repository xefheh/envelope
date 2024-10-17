using System.Diagnostics;
using TaskService.Domain.Enums;
using TaskService.Domain.Events;
using TaskService.Domain.Events.Base;
using TaskService.Domain.Interfaces;

namespace TaskService.Domain.Aggregates;

/// <summary>
/// Задача
/// </summary>
public class Task : IAggregate<BaseTaskEvent>
{
    /// <summary>
    /// Суррогатный ключ
    /// </summary>
    public Guid Id { get; set; }

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
    
    /// <summary>
    /// Состояние задачи
    /// </summary>
    public TaskGlobalState State { get; set; }
    
    /// <summary>
    /// Дата создание задачи
    /// </summary>
    public DateTime CreationDate { get; set; }
    
    /// <summary>
    /// Дата последнего обновления задачи
    /// </summary>
    public DateTime UpdateDate { get; set; }
    
    /// <summary>
    /// Восстановавление агрегата 
    /// </summary>
    /// <param name="event">Событие</param>
    /// <exception cref="InvalidOperationException">Ошибка, при неизвестном событии</exception>
    public void Apply(BaseTaskEvent @event)
    {
        switch (@event)
        {
            case BaseTaskCreated e:
                Apply(e);
                break;
            case BaseTaskRemoved e:
                Apply(e);
                break;
            case BaseTaskSentToCheck e:
                Apply(e);
                break;
            case BaseTaskUpdated e:
                Apply(e);
                break;
            case BaseTaskSentToGlobal e:
                Apply(e);
                break;
            case BaseTaskRefused e:
                Apply(e);
                break;
            default:
                throw new InvalidOperationException($"Unknown event type; {@event.GetType().Name}");
        }
    }

    private void Apply(BaseTaskCreated @event)
    {
        Id = @event.Id;
        Name = @event.Name;
        Description = @event.Description;
        Answer = @event.Answer;
        Author = @event.Author;
        Difficult = @event.Difficult;
        ExecutionTime = @event.ExecutionTime;
        CreationDate = @event.EventDate;
    }

    private void Apply(BaseTaskRemoved _)
    {
        Id = Guid.Empty;
    }

    private void Apply(BaseTaskSentToCheck _)
    {
        State = TaskGlobalState.WaitCheck;
    }

    private void Apply(BaseTaskSentToGlobal _)
    {
        State = TaskGlobalState.Global;
    }

    private void Apply(BaseTaskUpdated @event)
    {
        Name = @event.Name;
        Description = @event.Description;
        Answer = @event.Answer;
        Difficult = @event.Difficult;
        ExecutionTime = @event.ExecutionTime;
        UpdateDate = @event.EventDate;
    }

    private void Apply(BaseTaskRefused _)
    {
        State = TaskGlobalState.Local;
    }
}