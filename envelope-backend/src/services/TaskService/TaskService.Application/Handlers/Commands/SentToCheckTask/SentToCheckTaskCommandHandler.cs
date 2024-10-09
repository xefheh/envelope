using MediatR;
using TaskService.Application.Common;
using TaskService.Application.EventStore;
using TaskService.Application.Exceptions;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Commands.SentToCheckTask;

public class SentToCheckTaskCommandHandler : IRequestHandler<SentToCheckTaskCommand, Result<Unit>>
{
    private readonly ITaskEventStore _eventStore;

    public SentToCheckTaskCommandHandler(ITaskEventStore eventStore)
    {
        _eventStore = eventStore;
    }
    
    public async Task<Result<Unit>> Handle(SentToCheckTaskCommand request, CancellationToken cancellationToken)
    {
        var lastEvent = await _eventStore.GetLastOrDefaultEventAsync(request.Id);
        
        switch (lastEvent)
        {
            case null or TaskRemoved:
                return Result<Unit>.OnFailure(new NotFoundException(typeof(Task), request.Id));
            case TaskSentToCheck or TaskSentToGlobal:
                return Result<Unit>.OnFailure(new InvalidStateException(request.GetType()));
        }

        var refusedEvent = new TaskSentToCheck
        {
            Id = lastEvent.Id,
            VersionId = lastEvent.VersionId + 1,
            EventDate = DateTime.UtcNow
        };
        
        await _eventStore.AddEventAsync(refusedEvent);
        
        return Result<Unit>.OnSuccess(Unit.Value);
    }
}