namespace Envelope.Common.Messages.RequestMessages.Users;

/// <summary>
/// Сообщение-запрос на получение информации об пользователе по Id
/// </summary>
public class GetUserByIdRequestMessage
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid Id { get; set; }
}