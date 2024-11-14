using MediatR;
using Envelope.Common.ResultPattern;
using TaskService.Application.Responses.GlobalTaskProjections.GetGlobalProjection;

namespace TaskService.Application.Handlers.Queries.GlobalProjectionQueriesHandler.GetGlobalProjection;

public class GetGlobalProjectionQuery : IRequest<Result<GetGlobalProjectionResponse>>
{
    public Guid Id { get; set; }
}