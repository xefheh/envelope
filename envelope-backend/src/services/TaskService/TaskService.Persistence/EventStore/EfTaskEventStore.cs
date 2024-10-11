using Microsoft.EntityFrameworkCore;
using TaskService.Application.EventStore;
using TaskService.Domain.Interfaces;
using TaskService.Persistence.Contexts;

namespace TaskService.Persistence.EventStore;

public class EfTaskEventStore : ITaskEventStore
{
    private readonly TaskEventStoreContext _context;

    public EfTaskEventStore(TaskEventStoreContext context) => _context = context;
    
    public async Task AddEventAsync(ITaskEvent taskEvent, CancellationToken cancellationToken)
    {
        await _context.TaskEvents.AddAsync(taskEvent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ICollection<ITaskEvent>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken)
    {
        return await _context.TaskEvents
            .Where(e => e.Id == aggregateId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ITaskEvent?> GetLastOrDefaultEventAsync(Guid aggregateId, CancellationToken cancellationToken)
    {
        return await _context.TaskEvents
            .Where(e => e.Id == aggregateId)
            .OrderBy(e => e.VersionId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}