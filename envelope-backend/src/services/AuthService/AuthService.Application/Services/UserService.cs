using AuthService.Application.Exceptions;
using AuthService.Application.Repositories;
using AuthService.Application.Requests;
using AuthService.Application.Responses;
using AuthService.Application.Utilities;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using System.Text.RegularExpressions;

namespace AuthService.Application.Services;

public class UserService 
{
    private readonly IUserRepository _userRepository;

    private const string EmailPattern = @"^[A-z0-9._%+-]+@[A-z0-9-]+\.[A-z]{2,4}$";

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDTO> Register(RegisterRequest request)
    {
        await CheckAvailabilityUserData(request);

        var password = HashHelper.CalculateMD5HashForString(request.Password);

        var user = new User()
        {
            Id = Guid.NewGuid(),
            Nickname = request.Nickname,
            Email = request.Email,
            Password = password,
            Role = Role.Student
        };

        await _userRepository.Create(user);

        return new UserDTO()
        {
            UserId = user.Id,
            Nickname = user.Nickname,
            Token = JWTGenerator.CreateJWT(user.Nickname),
            Role = Role.Student.ToString()
        };
    }

    private async Task CheckAvailabilityUserData(RegisterRequest request)
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
    }

    public async Task<UserDTO> Login(LoginRequest request)
    {
        User? user = await FindUserByLogin(request.Login);

        if (user.Password != HashHelper.CalculateMD5HashForString(request.Password))
        {
            throw new InvalidPasswordException("Incorrect password!");
        }

        return new UserDTO()
        {
            UserId = user.Id,
            Nickname = user.Nickname,
            Token = JWTGenerator.CreateJWT(user.Nickname),
            Role = user.Role.ToString()
        };
    }

    private async Task<User> FindUserByLogin(string login)
    {
        User? user = null;

        if (Regex.IsMatch(login, EmailPattern))
        {
            user = await _userRepository.GetUserByEmail(login);

            if (user is null)
            {
                throw new EmailNotExistsException($"A user with this email: {login} does not exist!");
            }
        }
        else
        {
            user = await _userRepository.GetUserByNickname(login);

            if (user is null)
            {
                throw new UsernameNotExistsException($"A user with this nickname: {login} does not exist!");
            }
        }

        return user;
    }
}