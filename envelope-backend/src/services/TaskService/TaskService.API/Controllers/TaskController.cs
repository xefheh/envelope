using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskService.Application.Handlers.Commands.AddTask;
using TaskService.Application.Handlers.Commands.RefuseTask;
using TaskService.Application.Handlers.Commands.RemoveTask;
using TaskService.Application.Handlers.Commands.SentToCheckTask;
using TaskService.Application.Handlers.Commands.SentToGlobalTask;
using TaskService.Application.Handlers.Commands.UpdateTask;

namespace TaskService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public TaskController(IMediator mediator) => _mediator = mediator;

    [HttpPost("post")]
    [Authorize]
    public async Task<ActionResult<Guid>> AddTaskAsync(AddTaskCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<ActionResult> UpdateTaskAsync(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok();
        }
        
        return NotFound(result.Exception!.Message);
    }

    [HttpDelete("delete")]
    [Authorize]
    public async Task<ActionResult> RemoveTaskAsync(RemoveTaskCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        if (result.IsSuccess)
        {
            return Ok();
        }
        
        return NotFound(result.Exception!.Message);
    }

    [HttpPost("postToCheck")]
    [Authorize]
    public async Task<ActionResult> SentToCheckTaskAsync(SentToCheckTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        if (result.IsSuccess)
        {
            return Ok();
        }
        
        return NotFound(result.Exception!.Message);
    }

    [HttpPost("postToGlobal")]
    [Authorize]
    public async Task<ActionResult> SentToGlobalTaskAsync(SentToGlobalTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        if (result.IsSuccess)
        {
            return Ok();
        }
        
        return NotFound(result.Exception!.Message);
    }
    
    [HttpPost("refuse")]
    [Authorize]
    public async Task<ActionResult> RefuseTaskAsync(RefuseTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        if (result.IsSuccess)
        {
            return Ok();
        }
        
        return NotFound(result.Exception!.Message);
    }
}