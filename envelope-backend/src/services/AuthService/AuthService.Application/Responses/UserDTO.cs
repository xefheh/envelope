namespace AuthService.Application.Responses;

public class UserDTO
{
    public Guid UserID { get; set; }
    public required string UserName { get; set; }
    public required string Token { get; set; }
}
