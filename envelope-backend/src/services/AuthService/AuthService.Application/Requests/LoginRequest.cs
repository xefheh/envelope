namespace AuthService.Application.Requests;

public class LoginRequest
{
    /// <summary>
    /// Электронная почта, либо никнейм
    /// </summary>
    public required string Login { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
}