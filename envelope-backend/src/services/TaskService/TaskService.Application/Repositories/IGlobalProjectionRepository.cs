using TaskService.Domain.Projections;

namespace TaskService.Application.Repositories;

public interface IGlobalProjectionRepository
{
    Task AddAsync(GlobalTaskProjection projection);

    Task RemoveAsync(Guid id);
    
    Task UpdateAsync(GlobalTaskProjection projection);

    Task GetProjectionAsync();
}