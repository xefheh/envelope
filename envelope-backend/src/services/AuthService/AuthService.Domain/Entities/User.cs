namespace AuthService.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Nickname { get; set; } 
    public Guid Role { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}