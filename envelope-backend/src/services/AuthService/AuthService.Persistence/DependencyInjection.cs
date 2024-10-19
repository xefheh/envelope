using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Application.Repositories;
using AuthService.Persistence.Data;
using AuthService.Persistence.Exceptions;
using AuthService.Persistence.Repositories;

namespace AuthService.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AddUserConfigurations(services, configuration);

        return services;
    }

    private static void AddUserConfigurations(IServiceCollection services, IConfiguration configuration)
    {
        var dataBaseName = "UsersDataBase";

        var userDataBaseConnectionString = configuration.GetConnectionString(dataBaseName);

        if (userDataBaseConnectionString is null)
        {
            throw new NotFoundConnectionStringException($"{dataBaseName} not found");
        }

        services.AddDbContext<UserContext>(builder => builder.UseNpgsql(userDataBaseConnectionString));

        services.AddScoped<IUserRepository, UserRepository>();
    }
}