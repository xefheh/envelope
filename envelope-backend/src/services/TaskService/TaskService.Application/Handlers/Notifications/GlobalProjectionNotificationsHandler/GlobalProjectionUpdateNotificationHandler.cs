using MediatR;
using TaskService.Application.EventStore;
using TaskService.Application.Repositories;
using TaskService.Domain.Events;
using TaskService.Domain.Projections;

namespace TaskService.Application.Handlers.Notifications.GlobalProjectionNotificationsHandler;

public class GlobalProjectionUpdateNotificationHandler : INotificationHandler<TaskUpdated>
{
    private readonly ITaskEventStore _eventStore;
    private readonly IGlobalProjectionRepository _globalProjectionRepository;

    public GlobalProjectionUpdateNotificationHandler(ITaskEventStore eventStore,
        IGlobalProjectionRepository globalProjectionRepository)
    {
        _eventStore = eventStore;
        _globalProjectionRepository = globalProjectionRepository;
    }

    public async Task Handle(TaskUpdated notification, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(notification.Id);
        var task = new Domain.Aggregates.Task();

        foreach (var @event in events)
        {
            task.Apply(@event);
        }
        
        var globalTaskProjection = new GlobalTaskProjection
        {
            Id = task.Id,
            Answer = task.Answer,
            Author = task.Author,
            Description = task.Description,
            Difficult = task.Difficult,
            ExecutionTime = task.ExecutionTime,
            Name = task.Name,
            State = task.State,
            UpdateDate = notification.EventDate
        };
    
        await _globalProjectionRepository.UpdateAsync(globalTaskProjection);
    }
}