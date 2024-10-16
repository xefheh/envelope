namespace CoursesService.Application.Requests.CoursTasks;

public class AddCourseTaskRequest
{
    public Guid Task { get; set; }
    public Guid BlockId { get; set; }
}