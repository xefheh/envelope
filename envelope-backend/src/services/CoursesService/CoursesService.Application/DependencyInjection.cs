using CoursesService.Application.Services;
using CoursesService.Application.Services.Interfaces;
using Envelope.Integration.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoursesService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddServices(services);
        AddMessageBus(services, configuration);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseBlockService, CourseBlockService>();
        services.AddScoped<ICourseTaskService, CourseTaskService>();
    }
    
    private static void AddMessageBus(IServiceCollection services, IConfiguration configuration)
    {
        services.AddIntegrationMessageBus(configuration);
    }
}