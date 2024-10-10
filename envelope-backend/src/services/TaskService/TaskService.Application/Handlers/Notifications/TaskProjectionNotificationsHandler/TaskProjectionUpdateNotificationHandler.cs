using MediatR;
using TaskService.Application.EventStore;
using TaskService.Application.Mapping.Projections;
using TaskService.Application.Repositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Notifications.TaskProjectionNotificationsHandler;

public class TaskProjectionUpdateNotificationHandler : INotificationHandler<TaskUpdated>
{
    private readonly ITaskProjectionWriteOnlyRepository _repository;
    private readonly ITaskEventStore _eventStore;

    public TaskProjectionUpdateNotificationHandler(
        ITaskProjectionWriteOnlyRepository repository,
        ITaskEventStore eventStore)
    {
        _repository = repository;
        _eventStore = eventStore;
    }
    
    public async Task Handle(TaskUpdated notification, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(notification.Id, cancellationToken);
        
        var taskAggregate = new Domain.Aggregates.Task();

        foreach (var @event in events)
        {
            taskAggregate.Apply(@event);
        }
        
        var projection = ProjectionStaticMapper.MapToTaskProjection(taskAggregate);
        await _repository.AddAsync(projection, cancellationToken);
    } 
}  