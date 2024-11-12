using Envelope.Common.Enums;
using Envelope.Common.Exceptions;
using TaskService.Application.Exceptions;
using TaskService.Application.Handlers.Commands.AddTask;
using TaskService.Application.Handlers.Commands.RefuseTask;
using TaskService.Application.Handlers.Commands.RemoveTask;
using TaskService.Application.Handlers.Commands.SentToCheckTask;
using TaskService.Application.Handlers.Commands.SentToGlobalTask;
using TaskService.Application.Handlers.Commands.UpdateTask;
using TaskService.Tests.TaskServiceApplication.Tests.Infrastructure;

namespace TaskService.Tests.TaskServiceApplication.Tests.CommandHandlerTests;

public class NegativeDefaultCommandHandlersTests
{
    [Fact]
    public async Task DefaultHandlersTests_Negative_RemoveRemovedAggregateTest()
    {
        var mediator = MediatorFactory.Create();
        var authorGuid = Guid.NewGuid();
        
        var result = await mediator.Send(new AddTaskCommand { Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"});

        var id = result.Value;

        await mediator.Send(new RemoveTaskCommand { Id = id });
        
        var actual = await mediator.Send(new RemoveTaskCommand { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<NotFoundException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_RemoveNotExistedAggregateTest()
    {
        var mediator = MediatorFactory.Create();

        var id = Guid.NewGuid();

        await mediator.Send(new RemoveTaskCommand { Id = id });
        
        var actual = await mediator.Send(new RemoveTaskCommand { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<NotFoundException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_UpdateRemovedAggregateTest()
    {
        var mediator = MediatorFactory.Create();
        var authorGuid = Guid.NewGuid();
        
        var result = await mediator.Send(new AddTaskCommand { Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"});

        var id = result.Value;

        await mediator.Send(new RemoveTaskCommand { Id = id });
        
        var actual = await mediator.Send(new UpdateTaskCommand { Id = id,
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"});

        
        Assert.False(actual.IsSuccess);
        Assert.IsType<NotFoundException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_UpdateNotExistedAggregateTest()
    {
        var mediator = MediatorFactory.Create();

        var id = Guid.NewGuid();
        
        var actual = await mediator.Send(new UpdateTaskCommand { Id = id,
            Answer = "TestAnswer",
            Author = Guid.NewGuid(),
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"});

        
        Assert.False(actual.IsSuccess);
        Assert.IsType<NotFoundException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_SentToCheckRemovedAggregateTest()
    {
        var mediator = MediatorFactory.Create();
        var authorGuid = Guid.NewGuid();
        
        var result = await mediator.Send(new AddTaskCommand { Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"});

        var id = result.Value;

        await mediator.Send(new RemoveTaskCommand { Id = id });
        
        var actual = await mediator.Send(new SentToCheckTaskCommand { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<NotFoundException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_SentToCheckNotExistedAggregateTest()
    {
        var mediator = MediatorFactory.Create();

        var id = Guid.NewGuid();
        
        var actual = await mediator.Send(new SentToCheckTaskCommand { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<NotFoundException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_SentToCheckSendToCheckAggregateTest()
    {
        var mediator = MediatorFactory.Create();
        var authorGuid = Guid.NewGuid();
        
        var result = await mediator.Send(new AddTaskCommand { Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"});

        var id = result.Value;

        await mediator.Send(new SentToCheckTaskCommand { Id = id });
        
        var actual = await mediator.Send(new SentToCheckTaskCommand { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<InvalidStateException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_SentToCheckSendToGlobalAggregateTest()
    {
        var mediator = MediatorFactory.Create();
        var authorGuid = Guid.NewGuid();
        
        var result = await mediator.Send(new AddTaskCommand { Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"});

        var id = result.Value;

        await mediator.Send(new SentToCheckTaskCommand { Id = id });
        
        await mediator.Send(new SentToGlobalTaskCommand { Id = id });
        
        var actual = await mediator.Send(new SentToCheckTaskCommand { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<InvalidStateException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_SentToGlobalRemovedAggregateTest()
    {
        var mediator = MediatorFactory.Create();
        var authorGuid = Guid.NewGuid();
        
        var result = await mediator.Send(new AddTaskCommand { Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"});

        var id = result.Value;

        await mediator.Send(new RemoveTaskCommand { Id = id });
        
        var actual = await mediator.Send(new SentToGlobalTaskCommand() { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<NotFoundException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_SentToGlobalNotExistedAggregateTest()
    {
        var mediator = MediatorFactory.Create();

        var id = Guid.NewGuid();
        
        var actual = await mediator.Send(new SentToGlobalTaskCommand { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<NotFoundException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_SentToGlobalSendToGlobalAggregateTest()
    {
        var mediator = MediatorFactory.Create();
        var authorGuid = Guid.NewGuid();
        
        var result = await mediator.Send(new AddTaskCommand { Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"});

        var id = result.Value;

        await mediator.Send(new SentToCheckTaskCommand { Id = id });
        
        await mediator.Send(new SentToGlobalTaskCommand { Id = id });
        
        var actual = await mediator.Send(new SentToGlobalTaskCommand { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<InvalidStateException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_RefuseRemovedAggregateTest()
    {
        var mediator = MediatorFactory.Create();
        var authorGuid = Guid.NewGuid();
        
        var result = await mediator.Send(new AddTaskCommand { Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"});

        var id = result.Value;

        await mediator.Send(new RemoveTaskCommand { Id = id });
        
        var actual = await mediator.Send(new RefuseTaskCommand { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<NotFoundException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_RefuseNotExistedAggregateTest()
    {
        var mediator = MediatorFactory.Create();

        var id = Guid.NewGuid();
        
        var actual = await mediator.Send(new RefuseTaskCommand { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<NotFoundException>(actual.Exception);
    }
    
    [Fact]
    public async Task DefaultHandlersTests_Negative_RefuseSendToGlobalAggregateTest()
    {
        var mediator = MediatorFactory.Create();
        var authorGuid = Guid.NewGuid();
        
        var result = await mediator.Send(new AddTaskCommand { Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"});

        var id = result.Value;

        await mediator.Send(new SentToCheckTaskCommand { Id = id });
        
        await mediator.Send(new SentToGlobalTaskCommand { Id = id });
        
        var actual = await mediator.Send(new RefuseTaskCommand { Id = id });
        
        Assert.False(actual.IsSuccess);
        Assert.IsType<InvalidStateException>(actual.Exception);
    }
}