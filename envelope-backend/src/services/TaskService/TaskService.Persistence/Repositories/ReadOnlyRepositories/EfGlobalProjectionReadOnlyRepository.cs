using Microsoft.EntityFrameworkCore;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Domain.Projections;
using TaskService.Persistence.Contexts;

namespace TaskService.Persistence.Repositories.ReadOnlyRepositories;

public class EfGlobalProjectionReadOnlyRepository : IGlobalProjectionReadOnlyRepository
{
    private readonly TaskReadOnlyContext _context;

    public EfGlobalProjectionReadOnlyRepository(TaskReadOnlyContext context) =>
        _context = context;
    
    public async Task<GlobalTaskProjection?> GetProjectionAsync(Guid projectionId, CancellationToken cancellationToken) =>
        await _context.GlobalTaskProjections.FirstOrDefaultAsync(p => p.Id == projectionId, cancellationToken);

    public async Task<ICollection<GlobalTaskProjection>> GetProjectionsAsync(CancellationToken cancellationToken) =>
        await _context.GlobalTaskProjections.ToListAsync(cancellationToken);
}