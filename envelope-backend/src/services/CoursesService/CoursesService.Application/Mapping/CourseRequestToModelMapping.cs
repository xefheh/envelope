using CoursesService.Application.Requests.Course;
using CoursesService.Application.Responses.Courses.GetCourseResponse;
using CoursesService.Application.Responses.Courses.GetCoursesResponse;
using CoursesService.Domain.Entities;

namespace CoursesService.Application.Mapping;

public static class CourseRequestToModelMapping
{
    public static Course MapToModel(AddCourseRequest request) => new()
    {
        Name = request.Name,
        Description = request.Description
    };
    
    public static Course MapToModel(UpdateCourseRequest request) => new()
    {
        Id = request.Id,
        Name = request.Name,
        Description = request.Description
    };

    public static CourseSearchResponseData MapToResponse(Course course) => new()
    {
        Id = course.Id,
        Name = course.Name,
        UpdateDate = course.UpdateDate
    };

    public static CourseResponse MapToSingleResponse(Course course) => new()
    {
        Id = course.Id,
        Name = course.Name,
        Description = course.Description,
        IsOutOfDate = course.IsOutOfDate,
        UpdateDate = course.UpdateDate,
        StartDate = course.StartDate,
        Blocks = course.Blocks
            .Select(b =>
            new CourseBlockInfo
            {
                Id = b.Id,
                Description = b.Description,
                NameOfBlock = b.NameOfBlock,
                Blocks = b.Tasks
                    .Select(t =>
                    new CourseTaskInfo
                    {
                        Id = t.Id,
                        Task = t.Task
                    })
                    .ToList()
            })
            .ToList()
    };
}