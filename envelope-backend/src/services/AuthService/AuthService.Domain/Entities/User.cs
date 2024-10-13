using AuthService.Domain.Enums;

namespace AuthService.Domain.Entities;

public class User
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

    /// <summary>
    /// Пароль хэшированный
    /// </summary>
    public required string Password { get; set; }
}