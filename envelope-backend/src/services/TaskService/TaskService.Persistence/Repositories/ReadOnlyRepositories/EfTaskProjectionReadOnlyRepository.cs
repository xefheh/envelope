using Microsoft.EntityFrameworkCore;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Domain.Projections;
using TaskService.Persistence.Contexts;

namespace TaskService.Persistence.Repositories.ReadOnlyRepositories;

public class EfTaskProjectionReadOnlyRepository : ITaskProjectionReadOnlyRepository
{
    private readonly TaskReadOnlyContext _context;

    public EfTaskProjectionReadOnlyRepository(TaskReadOnlyContext context) =>
        _context = context;
    
    public async Task<TaskProjection?> GetProjectionAsync(Guid projectionId, CancellationToken cancellationToken)  =>
        await _context.TaskProjections.FirstOrDefaultAsync(p => p.Id == projectionId, cancellationToken);

    public async Task<ICollection<TaskProjection>> GetProjectionsAsync(Guid authorId, CancellationToken cancellationToken) =>
        await _context.TaskProjections.ToListAsync(cancellationToken);
}