using System.Reflection;
using CoursesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursesService.Persistence;

public class CourseContext: DbContext
{
    public CourseContext(DbContextOptions<CourseContext> options) : base(options) { }

    public DbSet<Course> Courses { get; set; } = null!;
    
    public DbSet<CourseBlock> CourseBlocks { get; set; } = null!;
    
    public DbSet<CourseTask> CourseTasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}