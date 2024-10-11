﻿using MediatR;
using TaskService.Domain.Enums;
using TaskService.Domain.Interfaces;

namespace TaskService.Domain.Events;

/// <summary>
/// Событие: задача создана
/// </summary>
public class TaskCreated : ITaskEvent, INotification
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

    public Guid Id { get; set; }
    
    public int VersionId { get; set; }
    public DateTime EventDate { get; set; }
}