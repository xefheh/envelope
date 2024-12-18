namespace CoursesService.Application.Responses.Courses.GetCoursesResponse;

public class CourseSearchResponseData
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public string Description { get; set; }
    public DateTime UpdateDate { get; set; }
}