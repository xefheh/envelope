using MediatR;
using TaskService.Application.Common;
using TaskService.Application.Responses.TaskProjections.GetTaskProjection;

namespace TaskService.Application.Handlers.Queries.TaskProjectionProjection.GetTaskProjection;

public class GetTaskProjectionQuery : IRequest<Result<GetTaskProjectionResponse>>
{
    public Guid AuthorId { get; set; }
    public Guid TaskId { get; set; }
}