using MediatR;
using Envelope.Common.ResultPattern;
using TaskService.Application.Responses.TaskProjections.GetTaskProjection;

namespace TaskService.Application.Handlers.Queries.TaskProjectionProjection.GetTaskProjection;

public class GetTaskProjectionQuery : IRequest<Result<GetTaskProjectionResponse>>
{
    public Guid TaskId { get; set; }
}