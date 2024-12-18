namespace CoursesService.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsOutOfDate { get; set; }
    public Guid AuthorId { get; set; }
    public ICollection<CourseBlock> Blocks { get; set; } = new List<CourseBlock>();
}