using CoursesService.Application.Requests.CoursTasks;
using CoursesService.Domain.Entities;

namespace CoursesService.Application.Mapping;

public static class CourseTaskRequestToModelMapping
{
    public static CourseTask MapToModel(AddCourseTaskRequest request) => new()
    {
        Task = request.Task,
        Block = new CourseBlock() { Id = request.BlockId }
    };
}