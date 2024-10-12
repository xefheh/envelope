using AuthService.Domain.Enums;

namespace AuthService.Application.Responses;

public class UserDTO
{
    public Guid UserId { get; set; }
    public required string Nickname { get; set; }
    public required string Role { get; set; }
    public required string Token { get; set; }
}