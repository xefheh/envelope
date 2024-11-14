using TagManagement.Domain.Entities;

namespace TagManagement.Application.Repositories
{
    public interface ITagRepository
    {
        Task AddTagAsync(Tag tag);
        Task<IEnumerable<Tag?>> GetTagsForEntityAsync(Guid entityId);
        //Task<ICollection<Tag?>> PredictTag(Guid entityId);
        Task<bool> RemoveTagAsync(Guid id);
    }
}