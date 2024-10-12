namespace AuthService.Application.Responses;

public class UserDTO
{
    public Guid UserID { get; set; }
    public required string Nickname { get; set; }
    public required string Token { get; set; }
}