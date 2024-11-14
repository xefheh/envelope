using Microsoft.Extensions.DependencyInjection;
using TagManagement.Application.Services;

namespace TagManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            AddService(services);
            return services;
        }

        private static void AddService(IServiceCollection services)
        {
            services.AddScoped<TagService>();
        }
    }
}
