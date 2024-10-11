using MediatR;
using TaskService.Application.Repositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Notifications.GlobalProjectionNotificationsHandler;

public class GlobalProjectionRemoveNotificationHandler : INotificationHandler<TaskRemoved>
{
    private readonly IGlobalProjectionWriteOnlyRepository _repository;

    public GlobalProjectionRemoveNotificationHandler(
        IGlobalProjectionWriteOnlyRepository globalProjectionWriteOnlyRepository)
    { 
        _repository = globalProjectionWriteOnlyRepository;
    }
     
    public async Task Handle(TaskRemoved notification, CancellationToken cancellationToken)
    {
        await _repository.RemoveAsync(notification.Id, cancellationToken);
    }
}