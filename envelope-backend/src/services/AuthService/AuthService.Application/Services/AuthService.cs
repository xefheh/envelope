using AuthService.Application.Config;
using AuthService.Application.Repositories;
using AuthService.Application.Requests;
using AuthService.Application.Responses;
using AuthService.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Application.Services;

public class AuthService (IUserRepository userRepository, IRoleRepository roleRepository)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<UserDTO> Login(LoginRequest request)
    {
        User? user = await _userRepository.GetUserByEmail(request.Login) ?? throw new Exception("Такой логин не найден!");

        if (user.Password != GetHashPassword(request.Password))
            throw new Exception("Неправильный пароль!");

        return new UserDTO
        {
            UserID = user.Id,
            UserName = user.Nickname,
            Token = CreateJWT(user.Nickname)
        };
    }

    public async Task<UserDTO> Register(RegisterRequest request)
    {
        string password = GetHashPassword(request.Password);
        Role defaultRole = await _roleRepository.GetDefaultRole();


        var user = new User()
        {
            Id = Guid.NewGuid(),
            Nickname = request.Nickname,
            Email = request.Email,
            Password = password,
            Role = defaultRole.Id
        };

        await _userRepository.Create(user);

        return new UserDTO
        {
            UserID = user.Id,
            UserName = user.Nickname,
            Token = CreateJWT(user.Nickname)
        };
    }

    private string CreateJWT(string username)
    {
        var claims = new List<Claim> { new(ClaimTypes.Name, username) };
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private string GetHashPassword(string password)
    {
        byte[] bytesPassword = Encoding.ASCII.GetBytes(password);
        byte[] hashPassword = MD5.HashData(bytesPassword);

        return string.Join(string.Empty, hashPassword.Select(e => e.ToString("X2")));
    }
}
