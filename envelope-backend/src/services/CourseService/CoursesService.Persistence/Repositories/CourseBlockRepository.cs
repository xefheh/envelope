using CoursesService.Application.Repositories;
using CoursesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursesService.Persistence.Repositories;

public class CourseBlockRepository: ICourseBlockRepository
{
    private readonly CourseContext _context;

    public CourseBlockRepository(CourseContext context) => _context = context;
    
    public async Task<Guid> AddAsync(CourseBlock courseBlock, CancellationToken cancellationToken)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseBlock.Course.Id, cancellationToken);

        if (course == default)
        {
            return Guid.Empty;
        }

        var blockId = Guid.NewGuid();
        courseBlock.Id = blockId;
        course.Blocks.Add(courseBlock);
        await _context.SaveChangesAsync(cancellationToken);
        
        return courseBlock.Id;
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var block = await _context.CourseBlocks.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        if (block == default)
        {
            return false;
        }
        _context.CourseBlocks.Remove(block);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(CourseBlock updatedCourseBlock, CancellationToken cancellationToken)
    {
        var courseBlock = await _context.CourseBlocks.FirstOrDefaultAsync(b => b.Id == updatedCourseBlock.Id, cancellationToken);
        if (courseBlock == default)
        {
            return false;
        }
        courseBlock.NameOfBlock = updatedCourseBlock.NameOfBlock;
        courseBlock.Description = updatedCourseBlock.Description;

        return true;
    }
}