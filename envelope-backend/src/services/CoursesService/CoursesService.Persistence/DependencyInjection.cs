using CoursesService.Application.Repositories;
using CoursesService.Persistence.Repositories;
using CoursesService.Persistence.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CoursesService.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AddCourseConfigurations(services, configuration);
        return services;
    }

    private static void AddCourseConfigurations(IServiceCollection services, IConfiguration configuration)
    {
        var dataBaseName = "CourseDataBase";

        var userDataBaseConnectionString = configuration.GetConnectionString(dataBaseName);

        if (userDataBaseConnectionString is null)
        {
            throw new NotFoundConnectionStringException($"{dataBaseName} not found");
        }

        services.AddDbContext<CourseContext>(builder => builder.UseNpgsql(userDataBaseConnectionString));

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseBlockRepository, CourseBlockRepository>();
        services.AddScoped<ICourseTaskRepository, CourseTaskRepository>();

    }
}