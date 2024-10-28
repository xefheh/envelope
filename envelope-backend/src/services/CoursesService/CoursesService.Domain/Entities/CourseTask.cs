namespace CoursesService.Domain.Entities;

public class CourseTask
{
    public Guid Id { get; set; }
    public Guid Task { get; set; }

    public CourseBlock Block { get; set; } = null!;
}