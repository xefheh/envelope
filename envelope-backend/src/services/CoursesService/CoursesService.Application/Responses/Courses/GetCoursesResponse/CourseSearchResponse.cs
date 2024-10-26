namespace CoursesService.Application.Responses.Courses.GetCoursesResponse;

public class CourseSearchResponse
{
    public ICollection<CourseSearchResponseData> Response { get; set; } = null!;
}