using MediatR;
using Envelope.Common.ResultPattern;
using TaskService.Application.Mapping.Responses;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Application.Responses.TaskProjections.GetAllTaskProjections;

namespace TaskService.Application.Handlers.Queries.TaskProjectionProjection.GetAllTaskProjection;

public class GetAllTaskProjectionQueryHandler : IRequestHandler<GetAllTaskProjectionQuery, Result<GetAllTaskProjectionsResponse>>
{
    private readonly ITaskProjectionReadOnlyRepository _repository;
    
    public GetAllTaskProjectionQueryHandler(ITaskProjectionReadOnlyRepository repository) =>
        _repository = repository;
    
    public async Task<Result<GetAllTaskProjectionsResponse>> Handle(GetAllTaskProjectionQuery request, CancellationToken cancellationToken)
    {
        var allProjections = await _repository.GetProjectionsAsync(request.AuthorId, cancellationToken);

        var responseInfos = allProjections
            .Select(TaskResponseMapping.MapToProjectionInfo)
            .ToList();

        var response = new GetAllTaskProjectionsResponse
        {
            Response = responseInfos
        };
        
        return Result<GetAllTaskProjectionsResponse>.OnSuccess(response);
    }
}