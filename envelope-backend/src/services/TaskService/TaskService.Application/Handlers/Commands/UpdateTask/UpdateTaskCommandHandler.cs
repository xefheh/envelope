using MediatR;
using TaskService.Application.Common;
using TaskService.Application.EventBus.Interfaces;
using TaskService.Application.EventStore;
using TaskService.Application.Exceptions;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Commands.UpdateTask;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result<Unit>>
{
    private readonly ITaskEventStore _eventStore;
    private readonly IEventBus _eventBus;

    public UpdateTaskCommandHandler(ITaskEventStore eventStore, IEventBus eventBus)
    {
        _eventStore = eventStore;
        _eventBus = eventBus;
    }
    
    public async Task<Result<Unit>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var lastEvent = await _eventStore.GetLastOrDefaultEventAsync(request.Id, cancellationToken);
        
        if (lastEvent is null or TaskRemoved)
        {
            return Result<Unit>.OnFailure(new NotFoundException(typeof(Task), request.Id));
        }

        var updateEvent = new TaskUpdated
        {
            Id = request.Id,
            VersionId = lastEvent.VersionId + 1,
            Answer = request.Answer,
            Description = request.Description,
            Difficult = request.Difficult,
            EventDate = DateTime.UtcNow,
            ExecutionTime = request.ExecutionTime,
            Name = request.Name,
        };
        
        await _eventStore.AddEventAsync(updateEvent, cancellationToken);
        
        await _eventBus.Publish(updateEvent, cancellationToken);
        
        return Result<Unit>.OnSuccess(Unit.Value);
    }
}