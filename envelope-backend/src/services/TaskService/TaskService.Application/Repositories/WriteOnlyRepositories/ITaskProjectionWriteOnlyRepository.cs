using TaskService.Domain.Projections;

namespace TaskService.Application.Repositories.WriteOnlyRepositories;

public interface ITaskProjectionWriteOnlyRepository
{
    Task AddAsync(TaskProjection projection, CancellationToken cancellationToken);

    Task RemoveAsync(Guid id, CancellationToken cancellationToken);
    
    Task UpdateAsync(TaskProjection projection, CancellationToken cancellationToken);
}