using MediatR;
using TaskService.Application.EventBus.Interfaces;

namespace TaskService.Application.EventBus;

public class EventBus : IEventBus
{
    private readonly IMediator _mediator;
    
    public EventBus(IMediator mediator) =>
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    public async Task Publish(INotification @event) =>
        await _mediator.Publish(@event);
}