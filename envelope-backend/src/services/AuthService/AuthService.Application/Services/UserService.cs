using AuthService.Application.Exceptions;
using AuthService.Application.Repositories;
using AuthService.Application.Requests;
using AuthService.Application.Responses;
using AuthService.Application.Utilities;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using System.Text.RegularExpressions;
using Envelope.Common.ResultPattern;

namespace AuthService.Application.Services;

public class UserService 
{
    private readonly IUserRepository _userRepository;

    private const string EmailPattern = @"^[A-z0-9._%+-]+@[A-z0-9-]+\.[A-z]{2,4}$";

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDTO>> Register(RegisterRequest request)
    {
        var exception =  await CheckAvailabilityUserData(request);

        if (exception is not null)
        {
            return Result<UserDTO>.OnFailure(exception);
        }

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

        var result = new UserDTO()
        {
            UserId = user.Id,
            Nickname = user.Nickname,
            Token = JWTHelper.CreateJWT(user.Nickname, user.Id.ToString()),
            Role = user.Role.ToString()
        };

        return Result<UserDTO>.OnSuccess(result);
    }

    private async Task<Exception?> CheckAvailabilityUserData(RegisterRequest request)
    {
        User? userByNickname = await _userRepository.GetUserByNickname(request.Nickname);

        if (userByNickname is not null)
        {
            return new UsernameExistsException($"This nickname is: {request.Nickname} is busy!");
        }

        if (!Regex.IsMatch(request.Email, EmailPattern))
        {
            return new InvalidEmailException($"Invalid email: {request.Email}!");
        }

        User? userByEmail = await _userRepository.GetUserByEmail(request.Email);

        if (userByEmail is not null)
        {
            return new EmailExistsException($"This email is: {request.Email} already in use!");
        }

        return null;
    }

    public async Task<Result<UserDTO>> Login(LoginRequest request)
    {
        Result<User> foundResult = await FindUserByLogin(request.Login);

        if (!foundResult.IsSuccess) 
        {
            return Result<UserDTO>.OnFailure(foundResult.Exception);
        }

        var user = foundResult.Value;
        if (user.Password != HashHelper.CalculateMD5HashForString(request.Password))
        {
            return Result<UserDTO>.OnFailure(new InvalidPasswordException("Incorrect password!"));
        }

        var result = new UserDTO()
        {
            UserId = user.Id,
            Nickname = user.Nickname,
            Token = JWTHelper.CreateJWT(user.Nickname, user.Id.ToString()),
            Role = user.Role.ToString()
        };

        return Result<UserDTO>.OnSuccess(result);
    }

    private async Task<Result<User>> FindUserByLogin(string login)
    {
        User? user = null;

        if (Regex.IsMatch(login, EmailPattern))
        {
            user = await _userRepository.GetUserByEmail(login);

            if (user is null)
            {
                var exception = new EmailNotExistsException($"A user with this email: {login} does not exist!");
                return Result<User>.OnFailure(exception);
            }
        }
        else
        {
            user = await _userRepository.GetUserByNickname(login);

            if (user is null)
            {
                var exception = new UsernameNotExistsException($"A user with this nickname: {login} does not exist!");
                return Result<User>.OnFailure(exception);
            }
        }

        return Result<User>.OnSuccess(user);
    }
}