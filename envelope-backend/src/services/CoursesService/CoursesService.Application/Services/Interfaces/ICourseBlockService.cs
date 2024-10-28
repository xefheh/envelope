using CoursesService.Application.Common;
using CoursesService.Application.Requests.CoursBlock;

namespace CoursesService.Application.Services.Interfaces;

public interface ICourseBlockService
{
    Task<Result<Guid>> AddAsync(AddCourseBlockRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> RemoveAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateAsync(UpdateCourseBlockRequest request, CancellationToken cancellationToken);
}