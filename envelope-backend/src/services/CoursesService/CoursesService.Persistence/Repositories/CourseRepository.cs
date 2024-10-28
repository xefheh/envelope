using CoursesService.Application.Repositories;
using CoursesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursesService.Persistence.Repositories;

public class CourseRepository: ICourseRepository
{
    private readonly CourseContext _context;

    public CourseRepository(CourseContext context) => _context = context;
    
    public async Task<Guid> AddAsync(Course course, CancellationToken cancellationToken)
    {
        var entry = await _context.AddAsync(course, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await entry.ReloadAsync(cancellationToken);
        return course.Id;
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (course == default)
        {
            return false;
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Course?> GetAsync(Guid id, CancellationToken cancellationToken) => 
        await _context.Courses.Include(c => c.Blocks)
            .ThenInclude(b => b.Tasks)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<IEnumerable<Course>> GetCoursesAsync(CancellationToken cancellationToken) =>
        await _context.Courses.ToListAsync(cancellationToken);

    public async Task<bool> UpdateAsync(Course updatedCourse, CancellationToken cancellationToken)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == updatedCourse.Id, cancellationToken);

        if (course == default)
        {
            return false;
        }

        course.Name = updatedCourse.Name;
        course.Description = updatedCourse.Description;

        return true;
    }
}