using MediatR;
using TaskService.Application.Common;
using TaskService.Application.Exceptions;
using TaskService.Application.Mapping.Responses;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Application.Responses.TaskProjections.GetTaskProjection;

namespace TaskService.Application.Handlers.Queries.TaskProjectionProjection.GetTaskProjection;

public class GetTaskProjectionQueryHandler : IRequestHandler<GetTaskProjectionQuery, Result<GetTaskProjectionResponse>>
{
    private readonly ITaskProjectionReadOnlyRepository _repository;

    public GetTaskProjectionQueryHandler(ITaskProjectionReadOnlyRepository repository) =>
        _repository = repository;
    
    public async Task<Result<GetTaskProjectionResponse>> Handle(GetTaskProjectionQuery request, CancellationToken cancellationToken)
    {
        var projection = await _repository.GetProjectionAsync(request.AuthorId, request.TaskId, cancellationToken);

        if (projection is null)
        {
            return Result<GetTaskProjectionResponse>.OnFailure(new NotFoundException(typeof(Task), request.TaskId));
        }

        var response = TaskResponseMapping.MapToTaskProjectionResponse(projection);

        return Result<GetTaskProjectionResponse>.OnSuccess(response);
    }
}