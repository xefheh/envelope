using TagManagementService.Tests.Infrastructure;
using TagManagement.Domain.Entities;
using TagManagement.Application.Request;
using TagManagement.Domain.Enums;
using TagManagementService.Tests.Infrastructure.Repositories;
using TagManagement.Application.Exceptions;

namespace TagManagementService.Tests.TagManagementServiceTests
{
    public class NegativeTagManagementServiceTests
    {
        private static void CreateDefaultTypeAndTag(CommonStorage commonStorage)
        {
            var tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Name = "имя тэга",
                Type = TagType.Task,
                EntityId = Guid.NewGuid(),
            };
            commonStorage.Tags.Add(tag);
        }

        [Fact]
        public async Task InvalidTagName_Add_ThrowInvalidTagNameException()
        {
            var (tagService, commonStorage) = DependencyInjection.CreateTagServiceAndRepository();
            CreateDefaultTypeAndTag(commonStorage);

            var request = new TagRequest()
            {
                TagId = Guid.NewGuid(),
                TagName = "",
                TagType = TagType.Task,
                EntityId = Guid.NewGuid(),
            };
             
            var result=await tagService.AddTagAsync(request);

            Assert.False(result.IsSuccess);
            Assert.IsType<InvalidNameException>(result.Exception);
        }
    }
}
