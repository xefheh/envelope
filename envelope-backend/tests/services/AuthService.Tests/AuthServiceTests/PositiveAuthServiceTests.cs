using AuthService.Tests.Infrastructure;
using AuthService.Application.Utilities;
using AuthService.Domain.Entities;
using AuthService.Application.Requests;

namespace AuthService.Tests.AuthServiceTests;

public class PositiveAuthServiceTests
{
    [Fact]
    public async Task ValidUserData_Registering_ReturnUserDTO()
    {
        var (authService, commonStorage) = DependencyInjection.CreateUserServiceAndRepository();

        var defaultRole = new Role()
        {
            Id = Guid.NewGuid(),
            Name = "Student"
        };
        commonStorage.Roles.Add(defaultRole);

        var request = new RegisterRequest() {
            Nickname = "Oleg",
            Email = "horosrab@mail.ru",
            Password = "parol",
        };
        
        var userDTO = await authService.Register(request);

        Assert.NotNull(userDTO);
        Assert.Equal(request.Nickname, userDTO.Nickname);
    }

    [Fact]
    public async Task ValidUserData_LoggingIn_ReturnUserDTO()
    {
        var (authService, commonStorage) = DependencyInjection.CreateUserServiceAndRepository();

        var defaultRole = new Role() { 
            Id = Guid.NewGuid(), 
            Name = "Student" 
        };
        commonStorage.Roles.Add(defaultRole);

        var user = new User() {
            Id = Guid.NewGuid(),
            Nickname = "Oleg",
            Email = "horosrab@mail.ru",
            Password = HashHelper.CalculateMD5HashForString("password"),
            Role = defaultRole.Id
        };
        commonStorage.Users.Add(user);

        var request = new LoginRequest()
        {
            Login = "horosrab@mail.ru",
            Password = "password"
        };

        var userDTO = await authService.Login(request);

        Assert.NotNull(userDTO);
        Assert.Equal(user.Nickname, userDTO.Nickname);
    }
}