using MediatR;
using Envelope.Common.ResultPattern;
using TaskService.Application.Responses.TaskProjections.GetAllTaskProjections;

namespace TaskService.Application.Handlers.Queries.TaskProjectionProjection.GetAllTaskProjection;

public class GetAllTaskProjectionQuery : IRequest<Result<GetAllTaskProjectionsResponse>>
{
    public Guid AuthorId { get; set; }
}