using TaskService.Application.EventStore;
using TaskService.Domain.Interfaces;

namespace TaskService.Tests.TaskServiceApplication.Tests.Infrastructure.EventStore;


public class MockTaskEventStore : ITaskEventStore
{
    private readonly List<ITaskEvent> _events = [];

    public async Task AddEventAsync(ITaskEvent taskEvent, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _events.Add(taskEvent), cancellationToken);
    }

    public async Task<ICollection<ITaskEvent>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_events.Where(e => e.Id == aggregateId).OrderBy(e => e.VersionId).ToList());
    }

    public async Task<ITaskEvent?> GetLastOrDefaultEventAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_events.OrderBy(e => e.VersionId).LastOrDefault(e => e.Id == aggregateId));
    }
}