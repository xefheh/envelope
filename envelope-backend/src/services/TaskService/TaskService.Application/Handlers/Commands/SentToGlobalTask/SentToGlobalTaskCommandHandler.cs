using MediatR;
using TaskService.Application.Common;
using TaskService.Application.EventBus.Interfaces;
using TaskService.Application.EventStore;
using TaskService.Application.Exceptions;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Commands.SentToGlobalTask;

public class SentToGlobalTaskCommandHandler : IRequestHandler<SentToGlobalTaskCommand, Result<Unit>>
{
    private readonly ITaskEventStore _eventStore;
    private readonly IEventBus _eventBus;

    public SentToGlobalTaskCommandHandler(ITaskEventStore eventStore, IEventBus eventBus)
    {
        _eventStore = eventStore;
        _eventBus = eventBus;
    }
    
    public async Task<Result<Unit>> Handle(SentToGlobalTaskCommand request, CancellationToken cancellationToken)
    {
        var lastEvent = await _eventStore.GetLastOrDefaultEventAsync(request.Id, cancellationToken);
        
        switch (lastEvent)
        {
            case null or BaseTaskRemoved:
                return Result<Unit>.OnFailure(new NotFoundException(typeof(Task), request.Id));
            case not BaseTaskSentToCheck:
                return Result<Unit>.OnFailure(new InvalidStateException(request.GetType()));
        }

        var sentToGlobalEvent = new BaseTaskSentToGlobal
        {
            Id = lastEvent.Id,
            VersionId = lastEvent.VersionId + 1,
            EventDate = DateTime.UtcNow
        };
        
        await _eventStore.AddEventAsync(sentToGlobalEvent, cancellationToken);
        
        await _eventBus.Publish(sentToGlobalEvent, cancellationToken);
        
        return Result<Unit>.OnSuccess(Unit.Value);
    }
}