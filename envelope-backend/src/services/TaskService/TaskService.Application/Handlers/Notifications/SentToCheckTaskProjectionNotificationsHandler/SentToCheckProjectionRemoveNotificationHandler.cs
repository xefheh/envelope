using MediatR;
using TaskService.Application.Repositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Events;
using TaskService.Domain.Interfaces;

namespace TaskService.Application.Handlers.Notifications.SentToCheckTaskProjectionNotificationsHandler;

public class SentToCheckProjectionRemoveNotificationHandler :
    INotificationHandler<TaskRemoved>,
    INotificationHandler<TaskRefused>,
    INotificationHandler<TaskSentToGlobal>
{
    private readonly ISentToCheckProjectionWriteOnlyRepository _repository;

    public SentToCheckProjectionRemoveNotificationHandler(ISentToCheckProjectionWriteOnlyRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(TaskRemoved notification, CancellationToken cancellationToken) =>
        await OnTaskRemoved(notification, cancellationToken);

    public async Task Handle(TaskRefused notification, CancellationToken cancellationToken) =>
        await OnTaskRemoved(notification, cancellationToken);

    public async Task Handle(TaskSentToGlobal notification, CancellationToken cancellationToken) =>
        await OnTaskRemoved(notification, cancellationToken);

    private async Task OnTaskRemoved(ITaskEvent @event, CancellationToken cancellationToken) =>
        await _repository.RemoveAsync(@event.Id, cancellationToken);
}