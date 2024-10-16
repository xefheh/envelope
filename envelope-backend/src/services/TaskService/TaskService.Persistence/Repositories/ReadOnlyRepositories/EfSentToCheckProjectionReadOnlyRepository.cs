using Microsoft.EntityFrameworkCore;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Domain.Projections;
using TaskService.Persistence.Contexts;

namespace TaskService.Persistence.Repositories.ReadOnlyRepositories;

public class EfSentToCheckProjectionReadOnlyRepository : ISentToCheckProjectionReadOnlyRepository
{
    private readonly TaskReadOnlyContext _context;
    
    public EfSentToCheckProjectionReadOnlyRepository(TaskReadOnlyContext context) =>
        _context = context;
    
    public async Task<SentToCheckTaskProjection?> GetProjectionAsync(Guid projectionId, CancellationToken cancellationToken) =>
        await _context.SentToCheckTaskProjections.FirstOrDefaultAsync(p => p.Id == projectionId, cancellationToken);

    public async Task<ICollection<SentToCheckTaskProjection>> GetProjectionsAsync(CancellationToken cancellationToken) =>
        await _context.SentToCheckTaskProjections.ToListAsync(cancellationToken);
}