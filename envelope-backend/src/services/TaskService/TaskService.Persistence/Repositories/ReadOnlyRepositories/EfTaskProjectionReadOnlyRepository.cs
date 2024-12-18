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

    public async Task<TaskProjection?> GetProjectionAsync(Guid projectionId, Guid? authorId, CancellationToken cancellationToken)
    {
        return await _context.TaskProjections.FirstOrDefaultAsync(p => p.Id == projectionId && (authorId == null || p.Author == authorId), cancellationToken);
    }

    public async Task<ICollection<TaskProjection>> GetProjectionsAsync(Guid? authorId, CancellationToken cancellationToken)
    {
        IQueryable<TaskProjection> filtredProjections = _context.TaskProjections;

        if(authorId == null)
        {
            filtredProjections = filtredProjections.Where(p => p.Author == authorId);
        }
        return await filtredProjections.ToListAsync(cancellationToken);
    }
}