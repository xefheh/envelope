using MediatR;
using TaskService.Application.Common;
using TaskService.Application.Responses.GlobalTaskProjections.GetGlobalProjection;
using TaskService.Application.Responses.SentToCheckProjections.GetSentToCheckProjection;

namespace TaskService.Application.Handlers.Queries.SentToCheckProjectionQueryHandler.GetSentToCheckProjection;

public class GetSentToCheckProjectionQuery : IRequest<Result<GetSentToCheckProjectionResponse>>
{
    public Guid Id { get; set; }
}