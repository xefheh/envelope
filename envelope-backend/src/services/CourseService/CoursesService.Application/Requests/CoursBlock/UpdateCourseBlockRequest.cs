namespace CoursesService.Application.Requests.CoursBlock;

public class UpdateCourseBlockRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}