using Envelope.Common.Enums;

namespace TaskService.Domain.Projections;

public class SentToCheckTaskProjection
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
}