using TagManagementService.Tests.Infrastructure;
using TagManagement.Domain.Entities;
using TagManagement.Application.Request;
using TagManagement.Domain.Enums;

namespace TagManagementService.Tests.TagManagementServiceTests
{
    public class PositiveTagManagementServiceTests
    {
        [Fact]
        public async Task ValidTagData_Add_ReturnTagDTO()
        {
            var (tagService, commonStorage) = DependencyInjection.CreateTagServiceAndRepository();

            var tag = new Tag 
            {
                Id = Guid.NewGuid(),
                Name = "имя тэга",
                Type = TagType.Task,
                EntityId = Guid.NewGuid(),
            };

            var request = new TagRequest
            {
                TagId = tag.Id,
                TagName = tag.Name,
                TagType = tag.Type,
                EntityId = tag.EntityId,
            };

            var result = await tagService.AddTagAsync(request);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ValidTagData_RemoveTag_IsSuccess()
        {
            var (tagService, commonStorage) = DependencyInjection.CreateTagServiceAndRepository();

            var tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Name = "название тэга",
                Type = TagType.Course,
                EntityId = Guid.NewGuid(),
            };
            commonStorage.Tags.Remove(tag);

            var request = new TagRequest
            {
                TagId = tag.Id,
                TagName = tag.Name,
                TagType = tag.Type,
                EntityId = tag.EntityId,
            };

            var isDeleted= await tagService.RemoveTagAsync(request);

            Assert.NotNull(isDeleted.Value);
            Assert.True(isDeleted.IsSuccess);
        }

        [Fact]
        public async Task ValidTagData_GetTagsForEntity_ReturnDTO()
        {
            var (tagService, commonStorage) = DependencyInjection.CreateTagServiceAndRepository();

            var entityId=Guid.NewGuid();
            var tags = new Tag[]
            {
                
                new Tag()
                {
                    Id = Guid.NewGuid(),
                    Name="имя тэга",
                    Type = TagType.Task,
                    EntityId = entityId,
                },
                new Tag()
                {
                    Id = Guid.NewGuid(),
                    Name="другое имя тэга",
                    Type = TagType.Task,
                    EntityId = entityId,
                }
            };

            var firstRequest = new TagRequest
            {
                TagId = tags[0].Id,
                TagName = tags[0].Name,
                TagType = tags[0].Type,
                EntityId = tags[0].EntityId,
            };

            var secondRequest = new TagRequest
            {
                TagId = tags[1].Id,
                TagName = tags[1].Name,
                TagType = tags[1].Type,
                EntityId = tags[1].EntityId,
            };

            var firstResult = await tagService.GetTagsForEntityAsync(firstRequest);
            var secondResult= await tagService.GetTagsForEntityAsync(firstRequest);

            Assert.Equal(firstRequest.EntityId, secondRequest.EntityId);
            Assert.Equal(firstRequest.TagType, secondRequest.TagType);
            Assert.NotEqual(firstRequest.TagName, secondRequest.TagName);
        }
    }
}
