namespace CoursesService.Application.Requests.Course;

public class UpdateCourseRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}