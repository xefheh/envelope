using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskService.Application.Handlers.Queries.GlobalProjectionQueriesHandler.GetAllGlobalProjection;
using TaskService.Application.Handlers.Queries.GlobalProjectionQueriesHandler.GetGlobalProjection;
using TaskService.Application.Handlers.Queries.SentToCheckProjectionQueryHandler.GetAllSentToCheckProjection;
using TaskService.Application.Handlers.Queries.SentToCheckProjectionQueryHandler.GetSentToCheckProjection;
using TaskService.Application.Handlers.Queries.TaskProjectionProjection.GetAllTaskProjection;
using TaskService.Application.Handlers.Queries.TaskProjectionProjection.GetTaskProjection;
using TaskService.Application.Responses.GlobalTaskProjections.GetAllGlobalProjections;
using TaskService.Application.Responses.GlobalTaskProjections.GetGlobalProjection;
using TaskService.Application.Responses.SentToCheckProjections.GetAllSentToCheckProjections;
using TaskService.Application.Responses.SentToCheckProjections.GetSentToCheckProjection;
using TaskService.Application.Responses.TaskProjections.GetAllTaskProjections;
using TaskService.Application.Responses.TaskProjections.GetTaskProjection;

namespace TaskService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskProjectionController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public TaskProjectionController(IMediator mediator) => _mediator = mediator;

    [HttpGet("global/getAll")]
    public async Task<ActionResult<GetAllGlobalProjectionsResponse>> GetAllGlobalProjections(
        [FromQuery] GetAllGlobalProjectionQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return result.Value!;
    }
    
    [HttpGet("global/get/{id:guid}")]
    public async Task<ActionResult<GetGlobalProjectionResponse>> GetGlobalProjection(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetGlobalProjectionQuery { Id = id }, cancellationToken);

        if (result.IsSuccess)
        {
            return result.Value!;
        }

        return NotFound(result.Exception!.Message);
    }
    
    [HttpGet("getAll")]
    public async Task<ActionResult<GetAllTaskProjectionsResponse>> GetAllProjections(
        [FromQuery] GetAllTaskProjectionQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return result.Value!;
    }
    
    [HttpGet("get/{id:guid}/{authorId:guid}")]
    public async Task<ActionResult<GetTaskProjectionResponse>> GetProjection(
        [FromRoute] Guid id,
        [FromRoute] Guid authorId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTaskProjectionQuery { TaskId = id }, cancellationToken);

        if (result.IsSuccess)
        {
            return result.Value!;
        }

        return NotFound(result.Exception!.Message);
    }
    
    [HttpGet("checked/getAll")]
    [Authorize]
    public async Task<ActionResult<GetAllSentToCheckProjectionsResponse>> GetAllSentToCheckProjections(
        [FromQuery] GetAllSentToCheckProjectionQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return result.Value!;
    }
    
    [HttpGet("checked/get/{id:guid}")]
    [Authorize]
    public async Task<ActionResult<GetSentToCheckProjectionResponse>> GetSentToCheckProjection(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetSentToCheckProjectionQuery { Id = id }, cancellationToken);

        if (result.IsSuccess)
        {
            return result.Value!;
        }

        return NotFound(result.Exception!.Message);
    }
}