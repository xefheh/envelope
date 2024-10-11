using MediatR;
using TaskService.Application.Common;
using TaskService.Application.Responses.GlobalTaskProjections.GetAllGlobalProjections;

namespace TaskService.Application.Handlers.Queries.GlobalProjectionQueriesHandler.GetAllGlobalProjection;

public class GetAllGlobalProjectionQuery : IRequest<Result<GetAllGlobalProjectionsResponse>>
{
    /// TODO: Тут нужны будут фильтры;
}