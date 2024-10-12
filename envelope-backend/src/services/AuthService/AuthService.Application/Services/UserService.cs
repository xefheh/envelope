using AuthService.Application.Config;
using AuthService.Application.Exceptions;
using AuthService.Application.Repositories;
using AuthService.Application.Requests;
using AuthService.Application.Responses;
using AuthService.Application.Utilities;
using AuthService.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace AuthService.Application.Services;

public class UserService (IUserRepository userRepository, IRoleRepository roleRepository)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    private const string EmailPattern = @"^[A-z0-9._%+-]+@[A-z0-9-]+\.[A-z]{2,4}$";

    public async Task<UserDTO> Register(RegisterRequest request)
    {
        User? userByNickname = await _userRepository.GetUserByNickname(request.Nickname);

        if (userByNickname is not null)
        {
            throw new UsernameExistsException($"This nickname is: {request.Nickname} is busy!");
        }

        if (!Regex.IsMatch(request.Email, EmailPattern))
        {
            throw new InvalidEmailException($"Invalid email: {request.Email}!");
        }

        User? userByEmail = await _userRepository.GetUserByEmail(request.Email);

        if (userByEmail is not null)
        {
            throw new EmailExistsException($"This email is: {request.Email} already in use!");
        }

        string password = HashHelper.CalculateMD5HashForString(request.Password);
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

        return new()
        {
            UserID = user.Id,
            Nickname = user.Nickname,
            Token = CreateJWT(user.Nickname)
        };
    }

    public async Task<UserDTO> Login(LoginRequest request)
    {
        User? user = null;

        if (Regex.IsMatch(request.Login, EmailPattern))
        {
            user = await _userRepository.GetUserByEmail(request.Login);

            if (user is null)
            {
                throw new EmailNotExistsException($"A user with this email: {request.Login} does not exist!");
            }
        }
        else
        {
            user = await _userRepository.GetUserByNickname(request.Login);

            if (user is null)
            {
                throw new UsernameNotExistsException($"A user with this nickname: {request.Login} does not exist!");

            }
        }


        if (user.Password != HashHelper.CalculateMD5HashForString(request.Password))
        {
            throw new InvalidPasswordException("Incorrect password!");
        }

        return new()
        {
            UserID = user.Id,
            Nickname = user.Nickname,
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
}