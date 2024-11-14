using Envelope.Common.Messages.RequestMessages.Tasks;
using Envelope.Common.Messages.ResponseMessages.Tasks;
using Envelope.Common.Queries;
using Envelope.Integration.Interfaces;
using Microsoft.Extensions.Hosting;
using TaskService.Application.BackgroundServices.Interfaces;
using TaskService.Application.Repositories.ReadOnlyRepositories;

namespace TaskService.Application.BackgroundServices;

public class TaskBackgroundService : BackgroundService, ITaskBackgroundService
{
    private readonly IMessageBus _messageBus;
    private readonly ITaskProjectionReadOnlyRepository _repository;

    public TaskBackgroundService(IMessageBus messageBus, ITaskProjectionReadOnlyRepository repository)
    {
        _messageBus = messageBus;
        _repository = repository;
    }

    public async Task<TaskResponseMessage> ResponseAsync(GetTaskByIdRequestMessage message)
    {
        var task = await _repository.GetProjectionAsync(message.Id, CancellationToken.None);

        if (task == default)
        {
            throw new KeyNotFoundException($"No projection with id {message.Id} was found.");
        }

        var response = new TaskResponseMessage
        {
            Id = task.Id,
            CreationDate = task.CreationDate,
            Description = task.Description,
            Difficult = task.Difficult,
            ExecutionTime = task.ExecutionTime,
            Name = task.Name,
            UpdateDate = task.UpdateDate,
        };
        
        return response;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messageBus.SubscribeResponseAsync<GetTaskByIdRequestMessage, TaskResponseMessage>(
            QueueNames.GetTaskQueue,
            ResponseAsync);
    }
}