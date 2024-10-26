namespace CoursesService.Application.Requests.CoursBlock;

public class AddCourseBlockRequest
{
    public Guid CourseId { get; set; }
    public string NameOfBlock { get; set; } = null!;
    public string Description { get; set; } = null!;
}