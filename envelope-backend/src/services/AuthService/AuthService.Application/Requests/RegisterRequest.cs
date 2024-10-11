namespace AuthService.Application.Requests;

public class RegisterRequest
{
    public required string Nickname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
