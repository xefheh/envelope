using CoursesService.Application.Services;
using CoursesService.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CoursesService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseBlockService, CourseBlockService>();
        services.AddScoped<ICourseTaskService, CourseTaskService>();
    }
}