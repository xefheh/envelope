using CoursesService.Application.Requests.Course;
using CoursesService.Application.Responses.Courses.GetCourseResponse;
using CoursesService.Application.Responses.Courses.GetCoursesResponse;
using CoursesService.Domain.Entities;
using Envelope.Common.Messages.ResponseMessages.Tasks;

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
        StartDate = course.StartDate
    };

    public static CourseBlockInfo MapToBlockInfo(CourseBlock courseBlock) => new()
    {
        Id = courseBlock.Id,
        Description = courseBlock.Description,
        NameOfBlock = courseBlock.NameOfBlock
    };

    public static CourseTaskInfo MapMessageToTaskInfo(TaskResponseMessage message) => new()
    {
        Id = message.Id,
        CreationDate = message.CreationDate,
        Description = message.Description,
        Difficult = message.Difficult,
        ExecutionTime = message.ExecutionTime,
        Name = message.Name,
        UpdateDate = message.UpdateDate
    };
}