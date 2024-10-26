using CoursesService.Domain.Entities;

namespace CoursesService.Application.Repositories;

public interface ICourseRepository
{
    Task<Guid> AddAsync(Course course, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken);
    Task<Course?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Course>> GetCoursesAsync(CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Course updatedCourse, CancellationToken cancellationToken);
}