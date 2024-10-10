using MediatR;
using TaskService.Application.Common;
using TaskService.Application.Handlers.Commands.AddTask;
using TaskService.Application.Handlers.Commands.RefuseTask;
using TaskService.Application.Handlers.Commands.RemoveTask;
using TaskService.Application.Handlers.Commands.SentToCheckTask;
using TaskService.Application.Handlers.Commands.SentToGlobalTask;
using TaskService.Application.Handlers.Commands.UpdateTask;
using TaskService.Domain.Enums;
using TaskService.Domain.Events;
using TaskService.Tests.TaskServiceApplication.Tests.Infrastructure;

namespace TaskService.Tests.TaskServiceApplication.Tests.CommandHandlerTests;

public class PositiveDefaultCommandHandlersTests
{
    [Fact]
    public async Task DefaultHandlersTests_Positive_CorrectVersionUpdate()
    {
        var (mediator, eventStore) = MediatorFactory.CreateWithStore();
        
        var authorGuid = Guid.NewGuid();

        List<bool> results = [];

        object result = await mediator.Send(new AddTaskCommand
        {
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });

        var id = ((Result<Guid>)result).Value;
        
        results.Add(((Result<Guid>)result).IsSuccess);

        result = await mediator.Send(new UpdateTaskCommand
        {
            Id = id,
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });
        results.Add(((Result<Unit>)result).IsSuccess);

        result = await mediator.Send(new SentToCheckTaskCommand { Id = id });
        results.Add(((Result<Unit>)result).IsSuccess);
        
        result = await mediator.Send(new RefuseTaskCommand { Id = id });
        results.Add(((Result<Unit>)result).IsSuccess);
        
        Assert.True(results.All(r => r));
        
        var lastEvent = await eventStore.GetLastOrDefaultEventAsync(id, CancellationToken.None);
        
        Assert.Equal(4, lastEvent!.VersionId);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Positive_CorrectStreamAggregateAssembled()
    {
        var (mediator, eventStore) = MediatorFactory.CreateWithStore();
        
        var authorGuid = Guid.NewGuid();

        var expectedTask = new Domain.Aggregates.Task
        {
            Id = Guid.Empty,
            Answer = "Test2Answer",
            Author = authorGuid,
            Description = "Test2Description",
            Difficult = Difficult.Medium,
            ExecutionTime = 50,
            Name = "TestName",
            State = TaskGlobalState.Global
        };
        
        var result = await mediator.Send(new AddTaskCommand
        {
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });

        var id = result.Value;

        await mediator.Send(new UpdateTaskCommand
        {
            Id = id,
            Answer = "Test2Answer",
            Author = authorGuid,
            Description = "Test2Description",
            Difficult = Difficult.Medium,
            ExecutionTime = 50,
            Name = "TestName"
        });

        await mediator.Send(new SentToCheckTaskCommand { Id = id });
        
        await mediator.Send(new SentToGlobalTaskCommand { Id = id });
        
        await mediator.Send(new RemoveTaskCommand() { Id = id });
        
        var events = await eventStore.GetEventsAsync(id, CancellationToken.None);

        var createdEvent = events.OfType<TaskCreated>().Last();

        expectedTask.CreationDate = createdEvent.EventDate;
        
        var lastUpdatedEvent = events.OfType<TaskUpdated>().Last();
        
        expectedTask.UpdateDate = lastUpdatedEvent.EventDate;

        var task = new Domain.Aggregates.Task();

        foreach (var @event in events)
        {
            task.Apply(@event);
        }
        
        Assert.Equivalent(expectedTask, task);
    }
}