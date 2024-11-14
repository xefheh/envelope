using Envelope.Common.Messages.RequestMessages.Users;
using Envelope.Common.Messages.ResponseMessages.Users;

namespace AuthService.Application.BackgroundServices.Interfaces;

public interface IAuthBackgroundService
{
    Task<UserInfoResponseMessage> ResponseMessage(GetUserByIdRequestMessage message);
}