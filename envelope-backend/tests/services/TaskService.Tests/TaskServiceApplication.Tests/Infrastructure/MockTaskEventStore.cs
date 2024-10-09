using TaskService.Application.EventStore;
using TaskService.Domain.Interfaces;

namespace TaskService.Tests.TaskServiceApplication.Tests.Infrastructure;


public class MockTaskEventStore : ITaskEventStore
{
    private readonly List<ITaskEvent> _events = [];

    public async Task AddEventAsync(ITaskEvent taskEvent)
    {
        _events.Add(taskEvent);
        await Task.CompletedTask;
    }

    public async Task<ICollection<ITaskEvent>> GetEventsAsync(Guid aggregateId)
    {
        return await Task.FromResult(_events.Where(e => e.Id == aggregateId).OrderBy(e => e.VersionId).ToList());
    }

    public async Task<ITaskEvent?> GetLastOrDefaultEventAsync(Guid aggregateId)
    {
        return await Task.FromResult(_events.OrderBy(e => e.VersionId).LastOrDefault(e => e.Id == aggregateId));
    }
}