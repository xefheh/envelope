using MediatR;
using TaskService.Application.Common;
using TaskService.Application.Responses.GlobalTaskProjections.GetAllGlobalProjections;
using TaskService.Application.Responses.SentToCheckProjections.GetAllSentToCheckProjections;

namespace TaskService.Application.Handlers.Queries.SentToCheckProjectionQueryHandler.GetAllSentToCheckProjection;

public class GetAllSentToCheckProjectionQuery : IRequest<Result<GetAllSentToCheckProjectionsResponse>>
{
    /// TODO: Тут нужны будут фильтры;
}