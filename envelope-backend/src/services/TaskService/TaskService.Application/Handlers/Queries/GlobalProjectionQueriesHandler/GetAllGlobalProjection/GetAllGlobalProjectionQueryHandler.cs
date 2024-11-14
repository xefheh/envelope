using MediatR;
using Envelope.Common.ResultPattern;
using TaskService.Application.Mapping.Responses;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Application.Responses.GlobalTaskProjections.GetAllGlobalProjections;

namespace TaskService.Application.Handlers.Queries.GlobalProjectionQueriesHandler.GetAllGlobalProjection;

public class GetAllGlobalProjectionQueryHandler : IRequestHandler<GetAllGlobalProjectionQuery, Result<GetAllGlobalProjectionsResponse>>
{
    private readonly IGlobalProjectionReadOnlyRepository _repository;
    
    public GetAllGlobalProjectionQueryHandler(IGlobalProjectionReadOnlyRepository repository) =>
        _repository = repository;
    
    public async Task<Result<GetAllGlobalProjectionsResponse>> Handle(GetAllGlobalProjectionQuery request, CancellationToken cancellationToken)
    {
        var allProjections = await _repository.GetProjectionsAsync(cancellationToken);

        var responseInfos = allProjections
            .Select(GlobalResponseMapping.MapToProjectionInfo)
            .ToList();

        var response = new GetAllGlobalProjectionsResponse
        {
            Response = responseInfos
        };
        
        return Result<GetAllGlobalProjectionsResponse>.OnSuccess(response);
    }
}