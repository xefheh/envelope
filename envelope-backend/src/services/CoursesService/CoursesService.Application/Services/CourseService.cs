using Envelope.Common.Exceptions;
using CoursesService.Application.Mapping;
using CoursesService.Application.Repositories;
using CoursesService.Application.Requests.Course;
using CoursesService.Application.Responses.Courses.GetCourseResponse;
using CoursesService.Application.Responses.Courses.GetCoursesResponse;
using CoursesService.Application.Services.Interfaces;
using CoursesService.Domain.Entities;
using Envelope.Common.ResultPattern;

namespace CoursesService.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repository;

    public CourseService(ICourseRepository repository) =>
        _repository = repository;
    
    public async Task<Result<Guid>> AddAsync(AddCourseRequest request, CancellationToken cancellationToken)
    {
        var course = CourseRequestToModelMapping.MapToModel(request);
        course.StartDate = DateTime.UtcNow;
        course.UpdateDate = DateTime.UtcNow;

        var id = await _repository.AddAsync(course, cancellationToken);

        return Result<Guid>.OnSuccess(id);
    }

    public async Task<Result<bool>> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeleted = await _repository.RemoveAsync(id, cancellationToken);

        return !isDeleted ?
            Result<bool>.OnFailure(new NotFoundException(typeof(Course), id)) :
            Result<bool>.OnSuccess(true);
    }

    public async Task<Result<CourseResponse>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = await _repository.GetAsync(id, cancellationToken);

        if (course == default)
        {
            return Result<CourseResponse>.OnFailure(new NotFoundException(typeof(Course), id));
        }

        var courseResponse = CourseRequestToModelMapping.MapToSingleResponse(course);

        return Result<CourseResponse>.OnSuccess(courseResponse);
    }

    public async Task<Result<CourseSearchResponse>> GetCoursesAsync(CancellationToken cancellationToken)
    {
        var courses = await _repository.GetCoursesAsync(cancellationToken);

        var coursesResponse = courses
            .Select(CourseRequestToModelMapping.MapToResponse)
            .ToList();

        return Result<CourseSearchResponse>.OnSuccess(new CourseSearchResponse { Response = coursesResponse });
    }

    public async Task<Result<bool>> UpdateAsync(UpdateCourseRequest request, CancellationToken cancellationToken)
    {
        var updatedCourse = CourseRequestToModelMapping.MapToModel(request);

        updatedCourse.UpdateDate = DateTime.UtcNow;

        var isUpdated = await _repository.UpdateAsync(updatedCourse, cancellationToken);

        return !isUpdated ?
            Result<bool>.OnFailure(new NotFoundException(typeof(Course), request.Id)) :
            Result<bool>.OnSuccess(true);
    }
}