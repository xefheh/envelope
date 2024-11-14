using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TagManagement.Persistence.Context;
using TagManagement.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;
using TagManagement.Application.Repositories;
using TagManagement.Persistence.Repositories;

namespace TagManagement.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            AddTagConfigurations(services, configuration);
            return services;
        }

        private static void AddTagConfigurations(IServiceCollection services, IConfiguration configuration)
        {
            var dataBaseName = "TagsDataBase";

            var userDataBaseConnectionString = configuration.GetConnectionString(dataBaseName);

            if (userDataBaseConnectionString is null)
            {
                throw new NotFoundConnectionStringException($"{dataBaseName} not found");
            }

            services.AddDbContext<TagContext>(builder => builder.UseNpgsql(userDataBaseConnectionString));
            services.AddScoped<ITagRepository, TagRepository>();
        }
    }
}
