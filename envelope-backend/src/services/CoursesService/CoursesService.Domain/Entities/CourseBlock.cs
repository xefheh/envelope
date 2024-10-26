namespace CoursesService.Domain.Entities;

public class CourseBlock
{
    public Guid Id { get; set; }
    public Course Course { get; set; } = null!;
    public string NameOfBlock { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<CourseTask> Tasks { get; set; } = new List<CourseTask>();

}