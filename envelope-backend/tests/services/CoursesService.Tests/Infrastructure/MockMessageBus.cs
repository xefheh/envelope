
using Envelope.Integration.Interfaces;

namespace CoursesService.Tests.Infrastructure;

public class MockMessageBus : IMessageBus
{
    public async Task SubscribeAsync<TMessage>(string queueName, Func<TMessage, Task> handleAsync)
    {
        await Task.Delay(5000);
    }

    public async Task PublishAsync<TMessage>(string queueName, TMessage message)
    {
        await Task.Delay(5000);
    }

    public async Task<TResponse> SendWithRequestAsync<TMessage, TResponse>(string queueName, TMessage message, int timeoutInMs)
    {
        return await Task.FromResult<TResponse>(default);
    }

    public async Task SubscribeResponseAsync<TMessage, TResponse>(string queueName, Func<TMessage, Task<TResponse>> handleAsync)
    {
        await Task.Delay(5000);
    }
}