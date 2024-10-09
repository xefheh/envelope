using System.Diagnostics;
using TaskService.Domain.Enums;
using TaskService.Domain.Events;
using TaskService.Domain.Interfaces;

namespace TaskService.Domain.Aggregates;

/// <summary>
/// Задача
/// </summary>
public class Task : IAggregate<ITaskEvent>
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
    /// Восстановавление агрегата 
    /// </summary>
    /// <param name="event">Событие</param>
    /// <exception cref="InvalidOperationException">Ошибка, при неизвестном событии</exception>
    public void Apply(ITaskEvent @event)
    {
        switch (@event)
        {
            case TaskCreated e:
                Apply(e);
                break;
            case TaskRemoved e:
                Apply(e);
                break;
            case TaskSentToCheck e:
                Apply(e);
                break;
            case TaskUpdated e:
                Apply(e);
                break;
            case TaskSentToGlobal e:
                Apply(e);
                break;
            case TaskRefused e:
                Apply(e);
                break;
            default:
                throw new InvalidOperationException($"Unknown event type; {@event.GetType().Name}");
        }
    }

    private void Apply(TaskCreated @event)
    {
        Id = @event.Id;
        Name = @event.Name;
        Description = @event.Description;
        Answer = @event.Answer;
        Author = @event.Author;
        Difficult = @event.Difficult;
        ExecutionTime = @event.ExecutionTime;
    }

    private void Apply(TaskRemoved _)
    {
        Id = Guid.Empty;
    }

    private void Apply(TaskSentToCheck _)
    {
        State = TaskGlobalState.WaitCheck;
    }

    private void Apply(TaskSentToGlobal _)
    {
        State = TaskGlobalState.Global;
    }

    private void Apply(TaskUpdated @event)
    {
        Name = @event.Name;
        Description = @event.Description;
        Answer = @event.Answer;
        Difficult = @event.Difficult;
        ExecutionTime = @event.ExecutionTime;
    }

    private void Apply(TaskRefused _)
    {
        State = TaskGlobalState.Local;
    }
}