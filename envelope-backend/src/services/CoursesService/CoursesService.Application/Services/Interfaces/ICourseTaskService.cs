using CoursesService.Application.Requests.CoursTasks;
using Envelope.Common.ResultPattern;

namespace CoursesService.Application.Services.Interfaces;

public interface ICourseTaskService
{
    Task<Result<Guid>> AddAsync(AddCourseTaskRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> RemoveAsync(Guid id, CancellationToken cancellationToken);
}