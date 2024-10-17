using TaskService.Application.EventStore;
using TaskService.Domain.Events.Base;
using TaskService.Domain.Interfaces;

namespace TaskService.Tests.TaskServiceApplication.Tests.Infrastructure.EventStore;


public class MockTaskEventStore : ITaskEventStore
{
    private readonly List<BaseTaskEvent> _events = [];

    public async Task AddEventAsync(BaseTaskEvent baseTaskEvent, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _events.Add(baseTaskEvent), cancellationToken);
    }

    public async Task<ICollection<BaseTaskEvent>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_events.Where(e => e.Id == aggregateId).OrderBy(e => e.VersionId).ToList());
    }

    public async Task<BaseTaskEvent?> GetLastOrDefaultEventAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_events.OrderBy(e => e.VersionId).LastOrDefault(e => e.Id == aggregateId));
    }
}