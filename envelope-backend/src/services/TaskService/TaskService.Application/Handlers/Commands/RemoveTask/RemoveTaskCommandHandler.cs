using Envelope.Common.Exceptions;
using MediatR;
using Envelope.Common.ResultPattern;
using TaskService.Application.EventBus.Interfaces;
using TaskService.Application.EventStore;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Commands.RemoveTask;

public class RemoveTaskCommandHandler : IRequestHandler<RemoveTaskCommand, Result<Unit>>
{
    private readonly ITaskEventStore _eventStore;
    private readonly IEventBus _eventBus;

    public RemoveTaskCommandHandler(ITaskEventStore eventStore, IEventBus eventBus)
    {
        _eventStore = eventStore;
        _eventBus = eventBus;
    }
    
    public async Task<Result<Unit>> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        var lastEvent = await _eventStore.GetLastOrDefaultEventAsync(request.Id, cancellationToken);
        
        if (lastEvent is null or BaseTaskRemoved)
        {
            return Result<Unit>.OnFailure(new NotFoundException(typeof(Task), request.Id));
        }

        var removeEvent = new BaseTaskRemoved
        {
            Id = request.Id,
            EventDate = DateTime.UtcNow,
            VersionId = lastEvent.VersionId + 1
        };
        
        await _eventStore.AddEventAsync(removeEvent, cancellationToken);
        
        await _eventBus.Publish(removeEvent, cancellationToken);
        
        return Result<Unit>.OnSuccess(Unit.Value);
    }
}