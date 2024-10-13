namespace AuthService.Application.Responses;

public class UserDTO
{
    /// <summary>
    /// Суррогатный ключ
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Никнейм
    /// </summary>
    public required string Nickname { get; set; }

    /// <summary>
    /// Роль
    /// </summary>
    public required string Role { get; set; }

    /// <summary>
    /// Токен (JWT)
    /// </summary>
    public required string Token { get; set; }
}