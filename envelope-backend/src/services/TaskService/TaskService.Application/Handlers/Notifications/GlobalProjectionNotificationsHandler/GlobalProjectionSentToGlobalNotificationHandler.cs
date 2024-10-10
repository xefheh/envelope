using MediatR;
using TaskService.Application.EventStore;
using TaskService.Application.Mapping.Projections;
using TaskService.Application.Repositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Events;
using TaskService.Domain.Projections;

namespace TaskService.Application.Handlers.Notifications.GlobalProjectionNotificationsHandler;

public class GlobalProjectionSentToGlobalNotificationHandler : INotificationHandler<TaskSentToGlobal>
{
    private readonly ITaskEventStore _eventStore;
    private readonly IGlobalProjectionWriteOnlyRepository _repository;

    public GlobalProjectionSentToGlobalNotificationHandler(ITaskEventStore eventStore,
        IGlobalProjectionWriteOnlyRepository globalProjectionWriteOnlyRepository)
    {
        _eventStore = eventStore;
        _repository = globalProjectionWriteOnlyRepository;
    }
    
    public async Task Handle(TaskSentToGlobal notification, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(notification.Id, cancellationToken);
        var task = new Domain.Aggregates.Task();

        foreach (var @event in events)
        {
            task.Apply(@event);
        }
        
        var globalProjection = ProjectionStaticMapper.MapToGlobalProjection(task);
        
        await _repository.AddAsync(globalProjection, cancellationToken);
    }
}