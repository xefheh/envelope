using System.Reflection.Metadata.Ecma335;
using TaskService.Application.Repositories;
using TaskService.Domain.Projections;

namespace TaskService.Tests.TaskServiceApplication.Tests.Infrastructure;

public class MockGlobalTaskRepository : IGlobalProjectionRepository
{
    private readonly List<GlobalTaskProjection> _globalTasks = [];
    
    public async Task AddAsync(GlobalTaskProjection projection)
    {
        _globalTasks.Add(projection);
    }

    public async Task RemoveAsync(Guid id)
    {
        _globalTasks.RemoveAll(t => t.Id == id);
    }

    public async Task UpdateAsync(GlobalTaskProjection projection)
    {
        if(_globalTasks.All(t => t.Id != projection.Id)) return;
        _globalTasks.RemoveAll(t => t.Id == projection.Id);
        _globalTasks.Add(projection);
    }

    public Task GetProjectionAsync()
    {
        throw new NotImplementedException();
    }
}