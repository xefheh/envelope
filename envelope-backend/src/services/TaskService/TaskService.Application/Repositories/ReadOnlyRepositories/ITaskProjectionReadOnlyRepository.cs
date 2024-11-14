using TaskService.Domain.Projections;

namespace TaskService.Application.Repositories.ReadOnlyRepositories;

public interface ITaskProjectionReadOnlyRepository
{
    Task<TaskProjection?> GetProjectionAsync(Guid projectionId, CancellationToken cancellationToken);
    Task<ICollection<TaskProjection>> GetProjectionsAsync(Guid authorId, CancellationToken cancellationToken);
}