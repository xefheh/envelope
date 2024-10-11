using MediatR;
using TaskService.Application.Common;
using TaskService.Application.Mapping.Responses;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Application.Responses.GlobalTaskProjections.GetAllGlobalProjections;
using TaskService.Application.Responses.SentToCheckProjections.GetAllSentToCheckProjections;

namespace TaskService.Application.Handlers.Queries.SentToCheckProjectionQueryHandler.GetAllSentToCheckProjection;

public class GetAllSentToCheckProjectionQueryHandler : IRequestHandler<GetAllSentToCheckProjectionQuery, Result<GetAllSentToCheckProjectionsResponse>>
{
    private readonly ISentToCheckProjectionReadOnlyRepository _repository;
    
    public GetAllSentToCheckProjectionQueryHandler(ISentToCheckProjectionReadOnlyRepository repository) =>
        _repository = repository;
    
    public async Task<Result<GetAllSentToCheckProjectionsResponse>> Handle(GetAllSentToCheckProjectionQuery request, CancellationToken cancellationToken)
    {
        var allProjections = await _repository.GetProjectionsAsync(cancellationToken);

        var responseInfos = allProjections
            .Select(SentToCheckResponseMapping.MapToProjectionInfo)
            .ToList();

        var response = new GetAllSentToCheckProjectionsResponse
        {
            Response = responseInfos
        };
        
        return Result<GetAllSentToCheckProjectionsResponse>.OnSuccess(response);
    }
}