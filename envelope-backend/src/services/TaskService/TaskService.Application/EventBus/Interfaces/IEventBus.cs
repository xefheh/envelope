using MediatR;
using TaskService.Domain.Interfaces;

namespace TaskService.Application.EventBus.Interfaces;

public interface IEventBus
{
    Task Publish(INotification @event, CancellationToken cancellationToken);
}