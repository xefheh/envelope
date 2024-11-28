using AuthService.Application.Exceptions;
using AuthService.Application.Requests;
using AuthService.Application.Utilities;
using AuthService.Domain.Entities;
using Envelope.Common.Enums;
using AuthService.Tests.Infrastructure;
using AuthService.Tests.Infrastructure.Repositories;

namespace AuthService.Tests.AuthServiceTests;

public class NegativeAuthServiceTetsts
{
    private static void CreateDefualtRoleAndUser(CommonStorage commonStorage)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Nickname = "Oleg",
            Email = "horosrab@mail.ru",
            Password = HashHelper.CalculateMD5HashForString("password"),
            Role = Role.Student
        };
        commonStorage.Users.Add(user);
    }

    [Fact]
    public async Task ExistentNickname_Registering_ThrowUsernameExistsException()
    {
        var (authService, commonStorage) = DependencyInjection.CreateUserServiceAndRepository();
        CreateDefualtRoleAndUser(commonStorage);

        var request = new RegisterRequest()
        {
            Nickname = "Oleg",
            Email = "any@mail.ru",
            Password = "password",
        };

        var result = await authService.Register(request);

        Assert.False(result.IsSuccess);
        Assert.IsType<UsernameExistsException>(result.Exception);
    }

    [Fact]
    public async Task InvalidEmail_Registering_ThrowInvalidEmailException()
    {
        var (authService, commonStorage) = DependencyInjection.CreateUserServiceAndRepository();
        CreateDefualtRoleAndUser(commonStorage);

        var request = new RegisterRequest()
        {
            Nickname = "Andrey",
            Email = "@mail.ru",
            Password = "password",
        };

        var result = await authService.Register(request);

        Assert.False(result.IsSuccess);
        Assert.IsType<InvalidEmailException>(result.Exception);
    }

    [Fact]
    public async Task ExistentEmail_Registering_ThrowEmailExistsException()
    {
        var (authService, commonStorage) = DependencyInjection.CreateUserServiceAndRepository();
        CreateDefualtRoleAndUser(commonStorage);

        var request = new RegisterRequest()
        {
            Nickname = "Andrey",
            Email = "horosrab@mail.ru",
            Password = "password",
        };

        var result = await authService.Register(request);

        Assert.False(result.IsSuccess);
        Assert.IsType<EmailExistsException>(result.Exception);
    }

    [Fact]
    public async Task NonExistentEmail_LoggingIn_ThrowEmailNotExistsException()
    {
        var (authService, commonStorage) = DependencyInjection.CreateUserServiceAndRepository();
        CreateDefualtRoleAndUser(commonStorage);

        var request = new LoginRequest()
        {
            Login = "noexistent@mail.ru",
            Password = "password"
        };

        var result = await authService.Login(request);

        Assert.False(result.IsSuccess);
        Assert.IsType<EmailNotExistsException>(result.Exception);
    }

    [Fact]
    public async Task NonExistentNickname_LoggingIn_ThrowUsernameNotExistsException()
    {
        var (authService, commonStorage) = DependencyInjection.CreateUserServiceAndRepository();
        CreateDefualtRoleAndUser(commonStorage);

        var request = new LoginRequest()
        {
            Login = "NonExistentNickname",
            Password = "password"
        };

        var result = await authService.Login(request);

        Assert.False(result.IsSuccess);
        Assert.IsType<UsernameNotExistsException>(result.Exception);
    }

    [Fact]
    public async Task InvalidPassword_LoggingIn_ThrowInvalidPasswordException()
    {
        var (authService, commonStorage) = DependencyInjection.CreateUserServiceAndRepository();
        CreateDefualtRoleAndUser(commonStorage);

        var request = new LoginRequest()
        {
            Login = "horosrab@mail.ru",
            Password = "1234"
        };

        var result = await authService.Login(request);

        Assert.False(result.IsSuccess);
        Assert.IsType<InvalidPasswordException>(result.Exception);
    }
}