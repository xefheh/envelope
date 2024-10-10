using TaskService.Application.Handlers.Commands.AddTask;
using TaskService.Application.Handlers.Commands.RemoveTask;
using TaskService.Application.Handlers.Commands.SentToCheckTask;
using TaskService.Application.Handlers.Commands.SentToGlobalTask;
using TaskService.Application.Handlers.Queries.GlobalProjectionQueriesHandler.GetAllGlobalProjection;
using TaskService.Application.Handlers.Queries.GlobalProjectionQueriesHandler.GetGlobalProjection;
using TaskService.Application.Handlers.Queries.SentToCheckProjectionQueryHandler.GetAllSentToCheckProjection;
using TaskService.Application.Handlers.Queries.SentToCheckProjectionQueryHandler.GetSentToCheckProjection;
using TaskService.Application.Handlers.Queries.TaskProjectionProjection.GetAllTaskProjection;
using TaskService.Application.Responses.GlobalTaskProjections.GetGlobalProjection;
using TaskService.Domain.Enums;
using TaskService.Tests.TaskServiceApplication.Tests.Infrastructure;

namespace TaskService.Tests.TaskServiceApplication.Tests.QueryAndNotificationHandlerTests;

public class PositiveDefaultQueryAndNotificationHandlerTests
{
    [Fact]
    public async Task DefaultQueryAndNotificationsHandlersTests_Positive_GetAfterAddSomeTasksWithGlobal()
    {
        var (mediator, eventStore, repository) = MediatorFactory.CreateWithStoreAndRepository();
        
        var authorGuid = Guid.NewGuid();

        List<bool> results = [];

        var firstTaskId = await mediator.Send(new AddTaskCommand
        {
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });
        
        var secondTask = await mediator.Send(new AddTaskCommand
        {
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });
        
        var thirdTaskId = await mediator.Send(new AddTaskCommand
        {
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });

        await mediator.Send(new SentToCheckTaskCommand() { Id = firstTaskId.Value });
        
        await mediator.Send(new SentToGlobalTaskCommand() { Id = firstTaskId.Value });

        var sentToCheckCount = await mediator.Send(new GetAllSentToCheckProjectionQuery());
        var allCount = await mediator.Send(new GetAllTaskProjectionQuery() { AuthorId = authorGuid });
        var globalCount = await mediator.Send(new GetAllGlobalProjectionQuery());
        
        var global = await mediator.Send(new GetGlobalProjectionQuery() { Id = firstTaskId.Value });

        const int expectedSentToCheckCount = 0;
        const int expectedAllTaskCount = 3;
        const int expectedGlobalTaskCount = 1;
        
        Assert.Equal(expectedSentToCheckCount, sentToCheckCount.Value?.Response.Count);
        Assert.Equal(expectedAllTaskCount, allCount.Value?.Response.Count);
        Assert.Equal(expectedGlobalTaskCount, globalCount.Value?.Response.Count);
        
        Assert.Equivalent(
            new GetGlobalProjectionResponse { Description = "TestDescription" },
            new GetGlobalProjectionResponse { Description = global.Value?.Description! });
    }
    
    [Fact]
    public async Task DefaultQueryAndNotificationsHandlersTests_Positive_GetAfterAddSomeTasksWithRemoval()
    {
        var (mediator, eventStore, repository) = MediatorFactory.CreateWithStoreAndRepository();
        
        var authorGuid = Guid.NewGuid();

        List<bool> results = [];

        var firstTaskId = await mediator.Send(new AddTaskCommand
        {
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });
        
        var secondTask = await mediator.Send(new AddTaskCommand
        {
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });
        
        var thirdTaskId = await mediator.Send(new AddTaskCommand
        {
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });

        await mediator.Send(new SentToCheckTaskCommand { Id = firstTaskId.Value });
        
        await mediator.Send(new SentToGlobalTaskCommand { Id = firstTaskId.Value });
        
        await mediator.Send(new RemoveTaskCommand { Id = firstTaskId.Value });
        
        var sentToCheckCount = await mediator.Send(new GetAllSentToCheckProjectionQuery());
        var allCount = await mediator.Send(new GetAllTaskProjectionQuery() { AuthorId = authorGuid });
        var globalCount = await mediator.Send(new GetAllGlobalProjectionQuery());
        
        var global = await mediator.Send(new GetGlobalProjectionQuery() { Id = firstTaskId.Value });

        const int expectedSentToCheckCount = 0;
        const int expectedAllTaskCount = 2;
        const int expectedGlobalTaskCount = 0;
        
        Assert.Equal(expectedSentToCheckCount, sentToCheckCount.Value?.Response.Count);
        Assert.Equal(expectedAllTaskCount, allCount.Value?.Response.Count);
        Assert.Equal(expectedGlobalTaskCount, globalCount.Value?.Response.Count);
        
        Assert.False(global.IsSuccess);
    }
    
    [Fact]
    public async Task DefaultQueryAndNotificationsHandlersTests_Positive_GetAfterAddSomeTasksWithSentToCheck()
    {
        var (mediator, eventStore, repository) = MediatorFactory.CreateWithStoreAndRepository();
        
        var authorGuid = Guid.NewGuid();
        
        var firstTaskId = await mediator.Send(new AddTaskCommand
        {
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });
        
        var secondTask = await mediator.Send(new AddTaskCommand
        {
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });
        
        var thirdTaskId = await mediator.Send(new AddTaskCommand
        {
            Answer = "TestAnswer",
            Author = authorGuid,
            Description = "TestDescription",
            Difficult = Difficult.Hard,
            ExecutionTime = null,
            Name = "TestName"
        });

        await mediator.Send(new SentToCheckTaskCommand { Id = firstTaskId.Value });
        
        await mediator.Send(new SentToGlobalTaskCommand { Id = firstTaskId.Value });
        
        await mediator.Send(new SentToCheckTaskCommand { Id = secondTask.Value });
        
        await mediator.Send(new RemoveTaskCommand { Id = firstTaskId.Value });
        
        var sentToCheckCount = await mediator.Send(new GetAllSentToCheckProjectionQuery());
        var allCount = await mediator.Send(new GetAllTaskProjectionQuery { AuthorId = authorGuid });
        var globalCount = await mediator.Send(new GetAllGlobalProjectionQuery());
        
        var global = await mediator.Send(new GetGlobalProjectionQuery { Id = firstTaskId.Value });

        const int expectedSentToCheckCount = 1;
        const int expectedAllTaskCount = 2;
        const int expectedGlobalTaskCount = 0;
        
        Assert.Equal(expectedSentToCheckCount, sentToCheckCount.Value?.Response.Count);
        Assert.Equal(expectedAllTaskCount, allCount.Value?.Response.Count);
        Assert.Equal(expectedGlobalTaskCount, globalCount.Value?.Response.Count);
        
        Assert.False(global.IsSuccess);
        
        var sentToCheck = await mediator.Send(new GetSentToCheckProjectionQuery { Id = secondTask.Value });
        
        Assert.True(sentToCheck.IsSuccess);
    }
}