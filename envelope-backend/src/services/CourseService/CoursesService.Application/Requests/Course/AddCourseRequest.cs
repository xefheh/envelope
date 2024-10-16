namespace CoursesService.Application.Requests.Course;

public class AddCourseRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}