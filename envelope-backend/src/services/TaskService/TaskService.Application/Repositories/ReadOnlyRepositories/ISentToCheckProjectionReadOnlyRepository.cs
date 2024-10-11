using TaskService.Domain.Projections;

namespace TaskService.Application.Repositories.ReadOnlyRepositories;

public interface ISentToCheckProjectionReadOnlyRepository
{
    Task<SentToCheckTaskProjection?> GetProjectionAsync(Guid projectionId, CancellationToken cancellationToken);
    Task<ICollection<SentToCheckTaskProjection>> GetProjectionsAsync(CancellationToken cancellationToken);
}