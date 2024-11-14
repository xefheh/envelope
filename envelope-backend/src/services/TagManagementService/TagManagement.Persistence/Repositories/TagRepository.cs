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

        public async Task AddTagAsync(Tag tag)
        {
            _context.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveTagAsync(Guid id)
        {
            var tag = _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (tag != null)
            {
                _context.Remove(tag);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Tag?>> GetTagsForEntityAsync(Guid entityId)
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
