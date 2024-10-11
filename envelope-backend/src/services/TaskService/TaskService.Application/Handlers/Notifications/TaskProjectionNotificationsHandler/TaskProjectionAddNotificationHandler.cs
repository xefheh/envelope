using MediatR;
using TaskService.Application.Mapping.Projections;
using TaskService.Application.Repositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Notifications.TaskProjectionNotificationsHandler;

public class TaskProjectionAddNotificationHandler : INotificationHandler<TaskCreated>
{
    private readonly ITaskProjectionWriteOnlyRepository _repository;

    public TaskProjectionAddNotificationHandler(ITaskProjectionWriteOnlyRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(TaskCreated notification, CancellationToken cancellationToken)
    {
        var taskAggregate = new Domain.Aggregates.Task();
        
        taskAggregate.Apply(notification);
        
        var projection = ProjectionStaticMapper.MapToTaskProjection(taskAggregate);
        await _repository.AddAsync(projection, cancellationToken);
    }
}