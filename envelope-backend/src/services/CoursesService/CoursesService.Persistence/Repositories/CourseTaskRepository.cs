using CoursesService.Application.Repositories;
using CoursesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursesService.Persistence.Repositories;

public class CourseTaskRepository: ICourseTaskRepository
{
    private readonly CourseContext _context;
    public CourseTaskRepository(CourseContext context) => _context = context;

    public async Task<Guid> AddAsync(CourseTask courseTask, CancellationToken cancellationToken)
    {
        var courseBlock = await _context.CourseBlocks
            .FirstOrDefaultAsync(cb => cb.Id == courseTask.Block.Id, cancellationToken);

        if (courseBlock == default)
        {
            return Guid.Empty;
        }

        var taskId = Guid.NewGuid();
        courseTask.Id = taskId;
        courseTask.Block = courseBlock;
        
        _context.CourseTasks.Add(courseTask);
        
        await _context.SaveChangesAsync(cancellationToken);
        return taskId;
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _context.CourseTasks.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        
        if (task == default)
        {
            return false;
        }

        _context.CourseTasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}