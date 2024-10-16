namespace CoursesService.Application.Responses.Courses.GetCoursesResponse;

public class CourseSearchResponseData
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime UpdateDate { get; set; }
}