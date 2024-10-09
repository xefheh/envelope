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
        var lastEvent = await _eventStore.GetLastOrDefaultEventAsync(request.Id);
        
        switch (lastEvent)
        {
            case null or TaskRemoved:
                return Result<Unit>.OnFailure(new NotFoundException(typeof(Task), request.Id));
            case not TaskSentToCheck:
                return Result<Unit>.OnFailure(new InvalidStateException(request.GetType()));
        }

        var refusedEvent = new TaskSentToGlobal
        {
            Id = lastEvent.Id,
            VersionId = lastEvent.VersionId + 1,
            EventDate = DateTime.UtcNow
        };
        
        await _eventStore.AddEventAsync(refusedEvent);
        
        await _eventBus.Publish(refusedEvent);
        
        return Result<Unit>.OnSuccess(Unit.Value);
    }
}