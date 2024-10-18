using MediatR;
using TaskService.Application.Common;
using TaskService.Application.EventBus.Interfaces;
using TaskService.Application.EventStore;
using TaskService.Application.Exceptions;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Commands.SentToCheckTask;

public class SentToCheckTaskCommandHandler : IRequestHandler<SentToCheckTaskCommand, Result<Unit>>
{
    private readonly ITaskEventStore _eventStore;
    private readonly IEventBus _eventBus;

    public SentToCheckTaskCommandHandler(
        ITaskEventStore eventStore,
        IEventBus eventBus)
    {
        _eventStore = eventStore;
        _eventBus = eventBus;
    }
    
    public async Task<Result<Unit>> Handle(SentToCheckTaskCommand request, CancellationToken cancellationToken)
    {
        var lastEvent = await _eventStore.GetLastOrDefaultEventAsync(request.Id, cancellationToken);
        
        switch (lastEvent)
        {
            case null or BaseTaskRemoved:
                return Result<Unit>.OnFailure(new NotFoundException(typeof(Task), request.Id));
            case BaseTaskSentToCheck or BaseTaskSentToGlobal:
                return Result<Unit>.OnFailure(new InvalidStateException(request.GetType()));
        }

        var sentToCheckEvent = new BaseTaskSentToCheck
        {
            Id = lastEvent.Id,
            VersionId = lastEvent.VersionId + 1,
            EventDate = DateTime.UtcNow
        };
        
        await _eventStore.AddEventAsync(sentToCheckEvent, cancellationToken);
        
        await _eventBus.Publish(sentToCheckEvent, cancellationToken);
        
        return Result<Unit>.OnSuccess(Unit.Value);
    }
}