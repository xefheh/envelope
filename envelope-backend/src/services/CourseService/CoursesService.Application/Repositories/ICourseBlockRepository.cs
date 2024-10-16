using CoursesService.Domain.Entities;

namespace CoursesService.Application.Repositories;

public interface ICourseBlockRepository
{
    Task<Guid> AddAsync(CourseBlock courseBlock, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(CourseBlock updatedCourseBlock, CancellationToken cancellationToken);
}