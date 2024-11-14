using Microsoft.EntityFrameworkCore;
using TagManagement.Domain.Entities;
using TagManagement.Persistence.Context;
using TagManagement.Application.Repositories;

namespace TagManagement.Persistence.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly TagContext _context;

        public TagRepository(TagContext context)
        {
            _context = context;
        }

        public async Task AddTag(Tag tag)
        {
            _context.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTag(Guid id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag?>> GetTagsForEntity(Guid entityId)
        {
            return await _context.Tags
                .Where(t => t.EntityId == entityId)
                .ToListAsync();
        }

        //public async Task<ICollection<Tag?>> PredictTag(Guid entityId)
        //{
        //    await return n;
        //}
    }
}
