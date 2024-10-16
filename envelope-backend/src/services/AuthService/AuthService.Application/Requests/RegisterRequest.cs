namespace AuthService.Application.Requests;

public class RegisterRequest
{
    /// <summary>
    /// Никнейм
    /// </summary>
    public required string Nickname { get; set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
}