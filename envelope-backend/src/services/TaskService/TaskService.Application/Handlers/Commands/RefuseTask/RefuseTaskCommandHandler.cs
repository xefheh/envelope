using MediatR;
using TaskService.Application.Common;
using TaskService.Application.EventStore;
using TaskService.Application.Exceptions;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Commands.RefuseTask;

public class RefuseTaskCommandHandler : IRequestHandler<RefuseTaskCommand, Result<Unit>>
{
    private readonly ITaskEventStore _eventStore;

    public RefuseTaskCommandHandler(ITaskEventStore eventStore)
    {
        _eventStore = eventStore;
    }
    
    public async Task<Result<Unit>> Handle(RefuseTaskCommand request, CancellationToken cancellationToken)
    {
        var lastEvent = await _eventStore.GetLastOrDefaultEventAsync(request.Id, cancellationToken);
        
        if (lastEvent is null or TaskRemoved)
        {
            return Result<Unit>.OnFailure(new NotFoundException(typeof(Task), request.Id));
        }

        if (lastEvent is not TaskSentToCheck)
        {
            return Result<Unit>.OnFailure(new InvalidStateException(request.GetType()));
        }

        var refusedEvent = new TaskRefused
        {
            Id = lastEvent.Id,
            VersionId = lastEvent.VersionId + 1,
            EventDate = DateTime.UtcNow
        };
        
        await _eventStore.AddEventAsync(refusedEvent, cancellationToken);
        
        return Result<Unit>.OnSuccess(Unit.Value);
    }
}