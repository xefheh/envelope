using MediatR;
using Envelope.Common.ResultPattern;
using TaskService.Application.EventBus.Interfaces;
using TaskService.Application.EventStore;
using TaskService.Domain.Events;
using Envelope.Integration.Interfaces;
using Envelope.Common.Queries;
using Envelope.Common.Messages.EventMessages.Tags;

namespace TaskService.Application.Handlers.Commands.AddTask;

public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, Result<Guid>>
{
    private readonly ITaskEventStore _eventStore;
    private readonly IEventBus _eventBus;
    private readonly IMessageBus _messageBus;

    public AddTaskCommandHandler(
        ITaskEventStore eventStore,
        IEventBus eventBus,
        IMessageBus messageBus) =>
        (_eventStore, _eventBus, _messageBus) = (eventStore, eventBus, messageBus);
    
    public async Task<Result<Guid>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var addEvent = new BaseTaskCreated
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
        
        await _eventStore.AddEventAsync(addEvent, cancellationToken);
        
        await _eventBus.Publish(addEvent, cancellationToken);

        foreach (var tag in request.Tags)
        {
            await _messageBus.PublishAsync(QueueNames.AddTagQueue, new AddTagMessage() { TagType = Envelope.Common.Enums.TagType.Task, EntityId = id, Name = tag });
        }

        return Result<Guid>.OnSuccess(id);
    }
}