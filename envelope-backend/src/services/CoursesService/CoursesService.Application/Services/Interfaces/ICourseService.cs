using CoursesService.Application.Requests.Course;
using CoursesService.Application.Responses.Courses.GetCourseResponse;
using CoursesService.Application.Responses.Courses.GetCoursesResponse;
using Envelope.Common.ResultPattern;

namespace CoursesService.Application.Services.Interfaces;

public interface ICourseService
{
    Task<Result<Guid>> AddAsync(AddCourseRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> RemoveAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<CourseResponse>> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<CourseSearchResponse>> GetCoursesAsync(CancellationToken cancellationToken);
    Task<Result<bool>> UpdateAsync(UpdateCourseRequest request, CancellationToken cancellationToken);
}