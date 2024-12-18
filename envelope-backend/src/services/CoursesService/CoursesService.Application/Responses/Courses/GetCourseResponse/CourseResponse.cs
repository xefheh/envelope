namespace CoursesService.Application.Responses.Courses.GetCourseResponse;

public class CourseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsOutOfDate { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;

    public ICollection<CourseBlockInfo> Blocks { get; set; } = new List<CourseBlockInfo>();
}