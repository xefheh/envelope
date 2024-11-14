using TagManagement.Domain.Entities;

namespace TagManagement.Application.Repositories
{
    public interface ITagRepository
    {
        Task AddTag(Tag tag);
        Task<IEnumerable<Tag?>> GetTagsForEntity(Guid entityId);
        //Task<ICollection<Tag?>> PredictTag(Guid entityId);
        Task RemoveTag(Guid id);
    }
}