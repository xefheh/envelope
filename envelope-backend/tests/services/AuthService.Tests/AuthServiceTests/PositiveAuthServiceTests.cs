using AuthService.Tests.Infrastructure;
using AuthService.Application.Utilities;
using AuthService.Domain.Entities;
using AuthService.Application.Requests;
using Envelope.Common.Enums;

namespace AuthService.Tests.AuthServiceTests;

public class PositiveAuthServiceTests
{
    [Fact]
    public async Task ValidUserData_Registering_ReturnUserDTO()
    {
        var (authService, commonStorage) = DependencyInjection.CreateUserServiceAndRepository();

        var request = new RegisterRequest() {
            Nickname = "Oleg",
            Email = "horosrab@mail.ru",
            Password = "parol",
        };
        
        var result = await authService.Register(request);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.NotEmpty(result.Value.Token);
        Assert.Equal(request.Nickname, result.Value.Nickname);
        Assert.Equal(Role.Student.ToString(), result.Value.Role);
        Assert.Equal(result.Value.UserId.ToString(), JWTHelper.GetClaimsFromToken(result.Value.Token)["Id"]);
    }

    [Fact]
    public async Task ValidUserData_LoggingIn_ReturnUserDTO()
    {
        var (authService, commonStorage) = DependencyInjection.CreateUserServiceAndRepository();

        var user = new User() {
            Id = Guid.NewGuid(),
            Nickname = "Oleg",
            Email = "horosrab@mail.ru",
            Password = HashHelper.CalculateMD5HashForString("password"),
            Role = Role.Teacher
        };
        commonStorage.Users.Add(user);

        var request = new LoginRequest()
        {
            Login = "horosrab@mail.ru",
            Password = "password"
        };

        var result = await authService.Login(request);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.NotEmpty(result.Value.Token);
        Assert.Equal(user.Nickname, result.Value.Nickname);
        Assert.Equal(Role.Teacher.ToString(), result.Value.Role);
        Assert.Equal(result.Value.UserId.ToString(), JWTHelper.GetClaimsFromToken(result.Value.Token)["Id"]);
    }
}