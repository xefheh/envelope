using MediatR;
using TaskService.Application.EventStore;
using TaskService.Application.Mapping.Projections;
using TaskService.Application.Repositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Notifications.SentToCheckTaskProjectionNotificationsHandler;

public class SentToCheckProjectionUpdateNotificationHandler : INotificationHandler<TaskUpdated>
{
    private readonly ISentToCheckProjectionWriteOnlyRepository _repository;
    private readonly ITaskEventStore _eventStore;

    public SentToCheckProjectionUpdateNotificationHandler(
        ISentToCheckProjectionWriteOnlyRepository repository,
        ITaskEventStore eventStore)
    {
        _repository = repository;
        _eventStore = eventStore;
    }
    
    public async Task Handle(TaskUpdated notification, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(notification.Id, cancellationToken);
        
        var task = new Domain.Aggregates.Task();

        foreach (var @event in events)
        {
            task.Apply(@event);
        }
        
        var sentToCheckTaskProjection = ProjectionStaticMapper.MapToSentToCheckTaskProjection(task);
    
        await _repository.UpdateAsync(sentToCheckTaskProjection, cancellationToken);
    }
}