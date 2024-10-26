using CoursesService.Domain.Entities;

namespace CoursesService.Application.Repositories;

public interface ICourseTaskRepository
{
    Task<Guid> AddAsync(CourseTask courseTask, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken);
}