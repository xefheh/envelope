using Microsoft.EntityFrameworkCore;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Projections;
using TaskService.Persistence.Contexts;

namespace TaskService.Persistence.Repositories.WriteOnlyRepositories;

public class EfSentToCheckProjectionWriteOnlyRepository : ISentToCheckProjectionWriteOnlyRepository
{
    private readonly TaskWriteOnlyContext _context;
    
    public EfSentToCheckProjectionWriteOnlyRepository(TaskWriteOnlyContext context) =>
        _context = context;
    
    public async Task AddAsync(SentToCheckTaskProjection projection, CancellationToken cancellationToken)
    {
        await _context.SentToCheckTaskProjections.AddAsync(projection, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var projection = await _context.SentToCheckTaskProjections.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        _context.Remove(projection!);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SentToCheckTaskProjection updatedProjection, CancellationToken cancellationToken)
    {
        var projection = await _context.SentToCheckTaskProjections.FirstOrDefaultAsync(p => p.Id == updatedProjection.Id, cancellationToken);
        projection!.Name = updatedProjection.Name;
        projection.Description = updatedProjection.Description;
        projection.Answer = updatedProjection.Answer;
        projection.ExecutionTime = updatedProjection.ExecutionTime;
        projection.Difficult = updatedProjection.Difficult;
        await _context.SaveChangesAsync(cancellationToken);
    }
}