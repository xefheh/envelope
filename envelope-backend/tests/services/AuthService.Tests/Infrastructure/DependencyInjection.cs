using AuthService.Application.Repositories;
using AuthService.Application.Services;
using AuthService.Tests.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Tests.Infrastructure;

public class DependencyInjection
{
    public static (UserService userService, CommonStorage repository) CreateUserServiceAndRepository()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<CommonStorage>();
        serviceCollection.AddSingleton<IUserRepository, MockCommonAuthRepository>();
        serviceCollection.AddSingleton<UserService>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        return (serviceProvider.GetService<UserService>()!, serviceProvider.GetService<CommonStorage>()!);
    }
}