using MediatR;
using TaskService.Application.Common;
using TaskService.Application.EventBus.Interfaces;
using TaskService.Application.EventStore;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Commands.AddTask;

public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, Result<Guid>>
{
    private readonly ITaskEventStore _eventStore;

    public AddTaskCommandHandler(ITaskEventStore eventStore) =>
        _eventStore = eventStore;
    
    public async Task<Result<Guid>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var addEvent = new TaskCreated
        {
            Id = id,
            Author = request.Author,
            Answer = request.Answer,
            Description = request.Description,
            Difficult = request.Difficult,
            EventDate = DateTime.UtcNow,
            ExecutionTime = request.ExecutionTime,
            Name = request.Name,
            VersionId = 1
        };
        
        await _eventStore.AddEventAsync(addEvent);
        
        return Result<Guid>.OnSuccess(id);
    }
}