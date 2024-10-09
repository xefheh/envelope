using MediatR;
using TaskService.Application.Repositories;
using TaskService.Domain.Events;

namespace TaskService.Application.Handlers.Notifications.GlobalProjectionNotificationsHandler;

public class GlobalProjectionRemoveNotificationHandler : INotificationHandler<TaskRemoved>
{
    private readonly IGlobalProjectionRepository _globalProjectionRepository;

    public GlobalProjectionRemoveNotificationHandler(
        IGlobalProjectionRepository globalProjectionRepository)
    { 
        _globalProjectionRepository = globalProjectionRepository;
    }
     
    public async Task Handle(TaskRemoved notification, CancellationToken cancellationToken)
    {
        await _globalProjectionRepository.RemoveAsync(notification.Id);
    }
}