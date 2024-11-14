using Envelope.Common.Enums;

namespace CoursesService.Application.Responses.Courses.GetCourseResponse;

public class CourseTaskInfo
{
    /// <summary>
    /// Id задачи
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
    /// Сложность
    /// </summary>
    public Difficult Difficult { get; set; }
    
    /// <summary>
    /// Время выполнения (в секундах)
    /// </summary>
    public int? ExecutionTime { get; set; }
    
    /// <summary>
    /// Дата создания задачи
    /// </summary>
    public DateTime CreationDate { get; set; }
    
    /// <summary>
    /// Дата обновления задачи
    /// </summary>
    public DateTime UpdateDate { get; set; }
}