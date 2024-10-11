using MediatR;
using TaskService.Application.Common;
using TaskService.Application.Exceptions;
using TaskService.Application.Mapping.Responses;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Application.Responses.GlobalTaskProjections.GetGlobalProjection;

namespace TaskService.Application.Handlers.Queries.GlobalProjectionQueriesHandler.GetGlobalProjection;

public class GetGlobalProjectionQueryHandler : IRequestHandler<GetGlobalProjectionQuery, Result<GetGlobalProjectionResponse>>
{
    private readonly IGlobalProjectionReadOnlyRepository _repository;

    public GetGlobalProjectionQueryHandler(IGlobalProjectionReadOnlyRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<GetGlobalProjectionResponse>> Handle(GetGlobalProjectionQuery request, CancellationToken cancellationToken)
    {
        var projection = await _repository.GetProjectionAsync(request.Id, cancellationToken);

        if (projection is null)
        {
            return Result<GetGlobalProjectionResponse>.OnFailure(new NotFoundException(typeof(Task), request.Id));
        }

        var response = GlobalResponseMapping.MapToGlobalResponse(projection);

        return Result<GetGlobalProjectionResponse>.OnSuccess(response);
    }
}