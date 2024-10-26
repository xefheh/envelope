using CoursesService.Application.Requests.CoursBlock;
using CoursesService.Domain.Entities;

namespace CoursesService.Application.Mapping;

public static class CourseBlockRequestToModelMapping
{
    public static CourseBlock MapToModel(AddCourseBlockRequest request) => new()
    {
        NameOfBlock = request.NameOfBlock,
        Description = request.Description,
        Course = new Course { Id = request.CourseId }
    };
    public static CourseBlock MapToModel(UpdateCourseBlockRequest request) => new()
    {
        Id = request.Id,
        NameOfBlock = request.Name,
        Description = request.Description
    };
}