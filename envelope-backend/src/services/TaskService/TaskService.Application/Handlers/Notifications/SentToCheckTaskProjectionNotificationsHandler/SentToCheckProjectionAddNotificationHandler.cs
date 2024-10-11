using MediatR;
using TaskService.Application.EventStore;
using TaskService.Application.Mapping.Projections;
using TaskService.Application.Repositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Events;
using TaskService.Domain.Projections;

namespace TaskService.Application.Handlers.Notifications.SentToCheckTaskProjectionNotificationsHandler;

public class SentToCheckProjectionAddNotificationHandler : INotificationHandler<TaskSentToCheck>
{
    private readonly ISentToCheckProjectionWriteOnlyRepository _repository;
    private readonly ITaskEventStore _eventStore;

    public SentToCheckProjectionAddNotificationHandler(
        ISentToCheckProjectionWriteOnlyRepository repository,
        ITaskEventStore eventStore)
    {
        _repository = repository;
        _eventStore = eventStore;
    }
    
    public async Task Handle(TaskSentToCheck notification, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(notification.Id, cancellationToken);
        var task = new Domain.Aggregates.Task();

        foreach (var @event in events)
        {
            task.Apply(@event);
        }
        
        var sentToCheckTaskProjection = ProjectionStaticMapper.MapToSentToCheckTaskProjection(task);
        
        await _repository.AddAsync(sentToCheckTaskProjection, cancellationToken);
    }
}