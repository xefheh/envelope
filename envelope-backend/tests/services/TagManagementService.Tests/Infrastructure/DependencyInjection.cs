using Microsoft.Extensions.DependencyInjection;
using TagManagement.Application.Repositories;
using TagManagement.Application.Services;
using TagManagementService.Tests.Infrastructure.Repositories;


namespace TagManagementService.Tests.Infrastructure
{
    /// <summary>
    /// Класс для внедрения зависимостей для тестов
    /// </summary>
    public class DependencyInjection
    {
        public static (TagService tagService, CommonStorage repository) CreateTagServiceAndRepository()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<CommonStorage>();
            serviceCollection.AddSingleton<ITagRepository, MockCommonTagRepository>();
            serviceCollection.AddSingleton<TagService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return (serviceProvider.GetService<TagService>()!, serviceProvider.GetService<CommonStorage>()!);
        }
    }
}
