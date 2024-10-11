using MediatR;
using TaskService.Application.Common;
using TaskService.Application.Responses.GlobalTaskProjections.GetGlobalProjection;

namespace TaskService.Application.Handlers.Queries.GlobalProjectionQueriesHandler.GetGlobalProjection;

public class GetGlobalProjectionQuery : IRequest<Result<GetGlobalProjectionResponse>>
{
    public Guid Id { get; set; }
}