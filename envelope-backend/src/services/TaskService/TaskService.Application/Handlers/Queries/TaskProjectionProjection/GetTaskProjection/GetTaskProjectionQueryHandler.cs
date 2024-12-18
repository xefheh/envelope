using Envelope.Common.Exceptions;
using MediatR;
using Envelope.Common.ResultPattern;
using TaskService.Application.Mapping.Responses;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Application.Responses.TaskProjections.GetTaskProjection;
using Envelope.Integration.Interfaces;
using Envelope.Common.Messages.RequestMessages.Users;
using Envelope.Common.Messages.ResponseMessages.Users;
using Envelope.Common.Queries;
using Envelope.Common.Messages.RequestMessages.Tags;

namespace TaskService.Application.Handlers.Queries.TaskProjectionProjection.GetTaskProjection;

public class GetTaskProjectionQueryHandler : IRequestHandler<GetTaskProjectionQuery, Result<GetTaskProjectionResponse>>
{
    private readonly ITaskProjectionReadOnlyRepository _repository;
    private readonly IMessageBus _messageBus;

    public GetTaskProjectionQueryHandler(ITaskProjectionReadOnlyRepository repository, IMessageBus messageBus)
    {
        _messageBus = messageBus;
        _repository = repository;
    }
    
    public async Task<Result<GetTaskProjectionResponse>> Handle(GetTaskProjectionQuery request, CancellationToken cancellationToken)
    {
        var projection = await _repository.GetProjectionAsync(request.TaskId, request.AuthorId, cancellationToken);

        if (projection is null)
        {
            return Result<GetTaskProjectionResponse>.OnFailure(new NotFoundException(typeof(Task), request.TaskId));
        }

        var authorNameResponse = await _messageBus
            .SendWithRequestAsync<
                GetUserByIdRequestMessage,
                UserInfoResponseMessage>(QueueNames.GetUserQueue, 
                    new GetUserByIdRequestMessage() { Id = projection.Author }, 10000);

        var response = TaskResponseMapping.MapToTaskProjectionResponse(projection);

        var tags = await _messageBus.SendWithRequestAsync<GetTagForEntityMessage, string[]>(
            QueueNames.GetTagQueue,
            new GetTagForEntityMessage() { EntityId = projection.Id, TagType = Envelope.Common.Enums.TagType.Task },
            10000);

        response.Tags = tags;
        response.AuthorName = authorNameResponse.Nickname;

        return Result<GetTaskProjectionResponse>.OnSuccess(response);
    }
}