using TagManagement.Application.Repositories;
using TagManagement.Domain.Entities;

namespace TagManagementService.Tests.Infrastructure.Repositories
{
    public class MockCommonTagRepository :ITagRepository
    {
        private readonly CommonStorage _commonStorage;
        public MockCommonTagRepository(CommonStorage commonStorage) 
        { 
            _commonStorage = commonStorage;
        }

        public async Task AddTag(Tag tag)
        {
            await Task.Run(() => _commonStorage.Tags.Add(tag));
        }

        public Task RemoveTag(Guid id)
        {
            var tag = _commonStorage.Tags.FirstOrDefault(t => t.Id == id);
            if (tag != null)
            {
                _commonStorage.Tags.Remove(tag);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public async Task<IEnumerable<Tag?>> GetTagsForEntity(Guid entityId)
        {
            return await Task.Run(() => _commonStorage.Tags
            .Where(t => t.EntityId == entityId)
            .ToList());
        }
    }
}
