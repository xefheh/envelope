using AuthService.Application.BackgroundServices.Interfaces;
using AuthService.Application.Repositories;
using Envelope.Common.Messages.RequestMessages.Users;
using Envelope.Common.Messages.ResponseMessages.Users;
using Envelope.Common.Queries;
using Envelope.Integration.Interfaces;
using Microsoft.Extensions.Hosting;

namespace AuthService.Application.BackgroundServices;

public class AuthBackgroundService : BackgroundService, IAuthBackgroundService
{
    private readonly IMessageBus _messageBus;
    private readonly IUserRepository _repository;

    public AuthBackgroundService(IMessageBus messageBus, IUserRepository repository)
    {
        _messageBus = messageBus;
        _repository = repository;
    }

    public async Task<UserInfoResponseMessage> ResponseMessage(GetUserByIdRequestMessage message)
    {
        var user = await _repository.GetUserById(message.Id);

        if (user == default)
        {
            throw new KeyNotFoundException($"User with id={message.Id} not found");
        }

        var userInfo = new UserInfoResponseMessage()
        {
            Id = user.Id,
            Nickname = user.Nickname,
            Role = user.Role,
            Email = user.Email
        };

        return userInfo;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messageBus.SubscribeResponseAsync<GetUserByIdRequestMessage, UserInfoResponseMessage>
            (QueueNames.GetUserQueue, ResponseMessage);
    }
}