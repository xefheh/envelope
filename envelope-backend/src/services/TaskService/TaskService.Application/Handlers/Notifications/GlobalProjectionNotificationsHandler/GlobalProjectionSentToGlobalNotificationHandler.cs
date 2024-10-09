using MediatR;
using TaskService.Application.EventStore;
using TaskService.Application.Repositories;
using TaskService.Domain.Events;
using TaskService.Domain.Projections;

namespace TaskService.Application.Handlers.Notifications.GlobalProjectionNotificationsHandler;

public class GlobalProjectionSentToGlobalNotificationHandler : INotificationHandler<TaskSentToGlobal>
{
    private readonly ITaskEventStore _eventStore;
    private readonly IGlobalProjectionRepository _globalProjectionRepository;

    public GlobalProjectionSentToGlobalNotificationHandler(ITaskEventStore eventStore,
        IGlobalProjectionRepository globalProjectionRepository)
    {
        _eventStore = eventStore;
        _globalProjectionRepository = globalProjectionRepository;
    }
    
    public async Task Handle(TaskSentToGlobal notification, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsAsync(notification.Id);
        var task = new Domain.Aggregates.Task();

        foreach (var @event in events)
        {
            task.Apply(@event);
        }
        
        var firstEvent = events.First();

        var globalTaskProjection = new GlobalTaskProjection
        {
            Id = task.Id,
            Answer = task.Answer,
            Author = task.Author,
            CreationDate = firstEvent.EventDate,
            Description = task.Description,
            Difficult = task.Difficult,
            ExecutionTime = task.ExecutionTime,
            Name = task.Name,
            State = task.State,
            UpdateDate = notification.EventDate
        };
        
        await _globalProjectionRepository.AddAsync(globalTaskProjection);
    }
}