namespace CoursesService.Application.Responses.Courses.GetCourseResponse;

public class CourseBlockInfo
{
    public Guid Id { get; set; }
    public string NameOfBlock { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public ICollection<CourseTaskInfo> Blocks { get; set; } = new List<CourseTaskInfo>();
}