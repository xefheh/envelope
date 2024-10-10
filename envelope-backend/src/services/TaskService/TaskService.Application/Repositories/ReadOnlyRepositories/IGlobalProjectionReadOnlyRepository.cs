using TaskService.Domain.Projections;

namespace TaskService.Application.Repositories.ReadOnlyRepositories;

public interface IGlobalProjectionReadOnlyRepository
{
    Task<GlobalTaskProjection?> GetProjectionAsync(Guid projectionId, CancellationToken cancellationToken);
    Task<ICollection<GlobalTaskProjection>> GetProjectionsAsync(CancellationToken cancellationToken);
}