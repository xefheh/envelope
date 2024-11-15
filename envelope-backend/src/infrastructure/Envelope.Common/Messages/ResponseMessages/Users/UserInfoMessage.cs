using Envelope.Common.Enums;

namespace Envelope.Common.Messages.ResponseMessages.Users;

/// <summary>
/// Ответ на сообщение-запрос получения пользователя
/// </summary>
public class UserInfoResponseMessage
{
    /// <summary>
    /// Суррогатный ключ
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Никнейм
    /// </summary>
    public required string Nickname { get; set; }

    /// <summary>
    /// Роль
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public required string Email { get; set; }
}