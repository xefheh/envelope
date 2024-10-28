using Microsoft.EntityFrameworkCore;
using TaskService.Application.EventStore;
using TaskService.Domain.Events.Base;
using TaskService.Domain.Interfaces;
using TaskService.Persistence.Contexts;

namespace TaskService.Persistence.EventStore;

public class EfTaskEventStore : ITaskEventStore
{
    private readonly TaskEventStoreContext _context;

    public EfTaskEventStore(TaskEventStoreContext context) => _context = context;
    
    public async Task AddEventAsync(BaseTaskEvent baseTaskEvent, CancellationToken cancellationToken)
    {
        await _context.TaskEvents.AddAsync(baseTaskEvent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ICollection<BaseTaskEvent>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken)
    {
        return await _context.TaskEvents
            .Where(e => e.Id == aggregateId)
            .OrderBy(e => e.VersionId)
            .ToListAsync(cancellationToken);
    }

    public async Task<BaseTaskEvent?> GetLastOrDefaultEventAsync(Guid aggregateId, CancellationToken cancellationToken)
    {
        return await _context.TaskEvents
            .Where(e => e.Id == aggregateId)
            .OrderByDescending(e => e.VersionId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}