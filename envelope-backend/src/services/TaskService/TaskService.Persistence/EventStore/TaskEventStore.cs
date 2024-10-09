using TaskService.Application.EventStore;
using TaskService.Domain.Interfaces;
using TaskService.Persistence.Factories.Abstraction;

namespace TaskService.Persistence.EventStore;

public class TaskEventStore : ITaskEventStore
{
    private readonly SqlConnectionFactory _connectionFactory;

    public TaskEventStore(SqlConnectionFactory connectionFactory) =>
        _connectionFactory = connectionFactory;
    
    public async Task AddEventAsync(ITaskEvent taskEvent)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<ITaskEvent>> GetEventsAsync(Guid aggregateId)
    {
        throw new NotImplementedException();
    }

    public async Task<ITaskEvent?> GetLastOrDefaultEventAsync(Guid aggregateId)
    {
        throw new NotImplementedException();
    }
}