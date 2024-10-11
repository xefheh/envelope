using MediatR;
using TaskService.Application.Common;
using TaskService.Application.Responses.TaskProjections.GetAllTaskProjections;

namespace TaskService.Application.Handlers.Queries.TaskProjectionProjection.GetAllTaskProjection;

public class GetAllTaskProjectionQuery : IRequest<Result<GetAllTaskProjectionsResponse>>
{
    public Guid AuthorId { get; set; }
}