using Envelope.Common.Exceptions;
using MediatR;
using Envelope.Common.ResultPattern;
using TaskService.Application.Exceptions;
using TaskService.Application.Mapping.Responses;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Application.Responses.SentToCheckProjections.GetSentToCheckProjection;

namespace TaskService.Application.Handlers.Queries.SentToCheckProjectionQueryHandler.GetSentToCheckProjection;

public class GetSentToCheckProjectionQueryHandler : IRequestHandler<GetSentToCheckProjectionQuery, Result<GetSentToCheckProjectionResponse>>
{
    private readonly ISentToCheckProjectionReadOnlyRepository _repository;

    public GetSentToCheckProjectionQueryHandler(ISentToCheckProjectionReadOnlyRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<GetSentToCheckProjectionResponse>> Handle(GetSentToCheckProjectionQuery request, CancellationToken cancellationToken)
    {
        var projection = await _repository.GetProjectionAsync(request.Id, cancellationToken);

        if (projection is null)
        {
            return Result<GetSentToCheckProjectionResponse>.OnFailure(new NotFoundException(typeof(Task), request.Id));
        }

        var response = SentToCheckResponseMapping.MapToSentToCheckResponse(projection);

        return Result<GetSentToCheckProjectionResponse>.OnSuccess(response);
    }
}