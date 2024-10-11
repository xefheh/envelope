using TaskService.Domain.Projections;

namespace TaskService.Application.Repositories.WriteOnlyRepositories;

public interface ISentToCheckProjectionWriteOnlyRepository
{
    Task AddAsync(SentToCheckTaskProjection projection, CancellationToken cancellationToken);

    Task RemoveAsync(Guid id, CancellationToken cancellationToken);
    
    Task UpdateAsync(SentToCheckTaskProjection projection, CancellationToken cancellationToken);
}