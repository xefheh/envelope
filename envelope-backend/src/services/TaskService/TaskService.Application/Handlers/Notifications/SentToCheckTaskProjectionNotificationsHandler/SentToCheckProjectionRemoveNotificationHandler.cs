using MediatR;
using TaskService.Application.Repositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Events;
using TaskService.Domain.Events.Base;
using TaskService.Domain.Interfaces;

namespace TaskService.Application.Handlers.Notifications.SentToCheckTaskProjectionNotificationsHandler;

public class SentToCheckProjectionRemoveNotificationHandler :
    INotificationHandler<BaseTaskRemoved>,
    INotificationHandler<BaseTaskRefused>,
    INotificationHandler<BaseTaskSentToGlobal>
{
    private readonly ISentToCheckProjectionWriteOnlyRepository _repository;

    public SentToCheckProjectionRemoveNotificationHandler(ISentToCheckProjectionWriteOnlyRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(BaseTaskRemoved notification, CancellationToken cancellationToken) =>
        await OnTaskRemoved(notification, cancellationToken);

    public async Task Handle(BaseTaskRefused notification, CancellationToken cancellationToken) =>
        await OnTaskRemoved(notification, cancellationToken);

    public async Task Handle(BaseTaskSentToGlobal notification, CancellationToken cancellationToken) =>
        await OnTaskRemoved(notification, cancellationToken);

    private async Task OnTaskRemoved(BaseTaskEvent @event, CancellationToken cancellationToken) =>
        await _repository.RemoveAsync(@event.Id, cancellationToken);
}