using MediatR;
using TaskService.Application.EventStore;
using TaskService.Application.Mapping.Projections;
using TaskService.Application.Repositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Events;
using TaskService.Domain.Projections;

namespace TaskService.Application.Handlers.Notifications.GlobalProjectionNotificationsHandler;

public class GlobalProjectionUpdateNotificationHandler : INotificationHandler<BaseTaskUpdated>
{
    private readonly ITaskEventStore _eventStore;
    private readonly IGlobalProjectionWriteOnlyRepository _repository;

    public GlobalProjectionUpdateNotificationHandler(ITaskEventStore eventStore,
        IGlobalProjectionWriteOnlyRepository repository)
    {
        _eventStore = eventStore;
        _repository = repository;
    }

    public async Task Handle(BaseTaskUpdated notification, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(notification.Id, cancellationToken);
        
        var task = new Domain.Aggregates.Task();

        foreach (var @event in events)
        {
            task.Apply(@event);
        }
        
        var globalTaskProjection = ProjectionStaticMapper.MapToGlobalProjection(task);
    
        await _repository.UpdateAsync(globalTaskProjection, cancellationToken);
    }
}