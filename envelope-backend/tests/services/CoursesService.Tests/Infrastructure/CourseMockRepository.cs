using CoursesService.Application.Repositories;
using CoursesService.Domain.Entities;

namespace CoursesService.Tests.Infrastructure;

public class CourseMockRepository : ICourseRepository, ICourseBlockRepository, ICourseTaskRepository
{
    private readonly List<Course> _courses = new();
    
    
    public Task<Guid> AddAsync(Course course, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        course.Id = id;
        _courses.Add(course);
        return Task.FromResult(id);
    }

    public Task<Guid> AddAsync(CourseBlock courseBlock, CancellationToken cancellationToken)
    {
        var courseBlockId = Guid.NewGuid();
        var course = _courses.FirstOrDefault(c => c.Id == courseBlock.Course.Id);
        courseBlock.Id = courseBlockId;
        course.Blocks.Add(courseBlock);
        return Task.FromResult(courseBlockId);
    }

    public Task<Guid> AddAsync(CourseTask courseTask, CancellationToken cancellationToken)
    {
        var course = _courses.FirstOrDefault(c => c.Blocks.Any(cc => cc.Id == courseTask.Block.Id));
        var courseBlock = course.Blocks.FirstOrDefault(cb => cb.Id == courseTask.Block.Id);
        var courseTaskId = Guid.NewGuid();
        courseTask.Id = courseTaskId;
        courseBlock.Tasks.Add(courseTask);
        return Task.FromResult(courseTaskId);
    }

    Task<bool> ICourseRepository.RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = _courses.FirstOrDefault(c => c.Id == id);
        if (course == default)
        {
            return Task.FromResult(false);
        }
        _courses.Remove(course);
        return Task.FromResult(true);
    }
    
    Task<bool> ICourseBlockRepository.RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = _courses.FirstOrDefault(c => c.Blocks.Any(cb=> cb.Id == id));
        if (course == default)
        {
            return Task.FromResult(false);
        }
        var block = course.Blocks.FirstOrDefault(b => b.Id == id);
        course.Blocks.Remove(block);
        return Task.FromResult(true);
    }
    
    Task<bool> ICourseTaskRepository.RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = _courses.FirstOrDefault(c => c.Blocks.Any(b=>b.Tasks.Any(t=>t.Id == id)));
        
        if (course == default)
        {
            return Task.FromResult(false);
        }
        
        var block = course.Blocks.FirstOrDefault(b => b.Tasks.Any(t=>t.Id == id));
        var task = block.Tasks.FirstOrDefault(t => t.Id == id);
        block.Tasks.Remove(task);
        return Task.FromResult(true);
    }

    public Task<bool> UpdateAsync(CourseBlock updatedCourseBlock, CancellationToken cancellationToken)
    {
        var course = _courses.FirstOrDefault(c => c.Blocks.Any(b => b.Id == updatedCourseBlock.Id));

        if (course == default)
        {
            return Task.FromResult(false);
        }

        var block = course.Blocks.FirstOrDefault(b => b.Id == updatedCourseBlock.Id);
        block.NameOfBlock = updatedCourseBlock.NameOfBlock;
        block.Description = updatedCourseBlock.Description;
        return Task.FromResult(true);
    }

    public Task<Course> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = _courses.FirstOrDefault(c => c.Id == id);
        return Task.FromResult<Course>(course);
    }

    public Task<IEnumerable<Course>> GetCoursesAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<Course>>(_courses);
    }

    public Task<bool> UpdateAsync(Course updatedCourse, CancellationToken cancellationToken)
    {
        var course = _courses.FirstOrDefault(c => c.Id == updatedCourse.Id);
        
        if (course == default)
        {
            return Task.FromResult(false);
        }

        course.Name = updatedCourse.Name;
        course.Description = updatedCourse.Description;
        return Task.FromResult(true); 
    }
}