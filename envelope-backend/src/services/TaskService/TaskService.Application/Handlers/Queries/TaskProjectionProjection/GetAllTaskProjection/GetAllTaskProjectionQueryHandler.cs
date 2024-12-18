using MediatR;
using Envelope.Common.ResultPattern;
using TaskService.Application.Mapping.Responses;
using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Application.Responses.TaskProjections.GetAllTaskProjections;
using Envelope.Integration.Interfaces;
using Envelope.Common.Messages.RequestMessages.Tags;
using Envelope.Common.Queries;

namespace TaskService.Application.Handlers.Queries.TaskProjectionProjection.GetAllTaskProjection;

public class GetAllTaskProjectionQueryHandler : IRequestHandler<GetAllTaskProjectionQuery, Result<GetAllTaskProjectionsResponse>>
{
    private readonly ITaskProjectionReadOnlyRepository _repository;
    private readonly IMessageBus _messageBus;

    public GetAllTaskProjectionQueryHandler(ITaskProjectionReadOnlyRepository repository, IMessageBus messageBus)
    {
        _repository = repository;
        _messageBus = messageBus;
    }
   
    
    public async Task<Result<GetAllTaskProjectionsResponse>> Handle(GetAllTaskProjectionQuery request, CancellationToken cancellationToken)
    {
        var allProjections = await _repository.GetProjectionsAsync(request.AuthorId, cancellationToken);

        var responseInfos = new List<GetAllTaskProjectionsInfo>();

        foreach(var projection in allProjections)
        {
            GetAllTaskProjectionsInfo result = TaskResponseMapping.MapToProjectionInfo(projection);
            var tags = await _messageBus.SendWithRequestAsync<GetTagForEntityMessage, string[]>(
                QueueNames.GetTagQueue,
                new GetTagForEntityMessage() { EntityId = projection.Id, TagType = Envelope.Common.Enums.TagType.Task },
            10000);

            result.Tags = tags;

            responseInfos.Add(result);
        }

        var response = new GetAllTaskProjectionsResponse
        {
            Response = responseInfos
        };
        
        return Result<GetAllTaskProjectionsResponse>.OnSuccess(response);
    }
}