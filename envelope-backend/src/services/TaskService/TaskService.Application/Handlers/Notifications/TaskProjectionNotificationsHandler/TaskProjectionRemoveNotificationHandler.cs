using MediatR;
using TaskService.Application.Repositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Notifications.TaskProjectionNotificationsHandler;

public class TaskProjectionRemoveNotificationHandler : INotificationHandler<TaskRemoved>
{
    private readonly ITaskProjectionWriteOnlyRepository _repository;

    public TaskProjectionRemoveNotificationHandler(ITaskProjectionWriteOnlyRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(TaskRemoved notification, CancellationToken cancellationToken)
    {
        await _repository.RemoveAsync(notification.Id, cancellationToken);
    }
}