using TaskService.Domain.Projections;

namespace TaskService.Application.Repositories.WriteOnlyRepositories;

public interface IGlobalProjectionWriteOnlyRepository
{
    Task AddAsync(GlobalTaskProjection projection, CancellationToken cancellationToken);

    Task RemoveAsync(Guid id, CancellationToken cancellationToken);
    
    Task UpdateAsync(GlobalTaskProjection projection, CancellationToken cancellationToken);
}