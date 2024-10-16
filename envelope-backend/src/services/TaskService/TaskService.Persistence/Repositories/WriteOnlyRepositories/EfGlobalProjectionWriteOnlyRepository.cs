using Microsoft.EntityFrameworkCore;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Projections;
using TaskService.Persistence.Contexts;

namespace TaskService.Persistence.Repositories.WriteOnlyRepositories;

public class EfGlobalProjectionWriteOnlyRepository : IGlobalProjectionWriteOnlyRepository
{
    private readonly TaskWriteOnlyContext _context;
    
    public EfGlobalProjectionWriteOnlyRepository(TaskWriteOnlyContext context) =>
        _context = context;

    public async Task AddAsync(GlobalTaskProjection projection, CancellationToken cancellationToken)
    {
        await _context.GlobalTaskProjections.AddAsync(projection, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var projection = await _context.GlobalTaskProjections.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        _context.Remove(projection!);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(GlobalTaskProjection updatedProjection, CancellationToken cancellationToken)
    {
        var projection = await _context.GlobalTaskProjections.FirstOrDefaultAsync(p => p.Id == updatedProjection.Id, cancellationToken);
        projection!.Name = updatedProjection.Name;
        projection.Description = updatedProjection.Description;
        projection.Answer = updatedProjection.Answer;
        projection.CreationDate = updatedProjection.CreationDate;
        projection.ExecutionTime = updatedProjection.ExecutionTime;
        projection.UpdateDate = updatedProjection.UpdateDate;
        projection.Difficult = updatedProjection.Difficult;
        await _context.SaveChangesAsync(cancellationToken);
    }
}