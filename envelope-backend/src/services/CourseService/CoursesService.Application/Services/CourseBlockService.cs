using CoursesService.Application.Common;
using CoursesService.Application.Exceptions;
using CoursesService.Application.Mapping;
using CoursesService.Application.Repositories;
using CoursesService.Application.Requests.CoursBlock;
using CoursesService.Application.Services.Interfaces;
using CoursesService.Domain.Entities;

namespace CoursesService.Application.Services;

public class CourseBlockService: ICourseBlockService
{
    private readonly ICourseBlockRepository _repository;

    public CourseBlockService(ICourseBlockRepository repository) =>
        _repository = repository;
    public async Task<Result<Guid>> AddAsync(AddCourseBlockRequest request, CancellationToken cancellationToken)
    {
        var block = CourseBlockRequestToModelMapping.MapToModel(request);
        var id = await _repository.AddAsync(block, cancellationToken);
        return Result<Guid>.OnSuccess(id);
    }

    public async Task<Result<bool>> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeleted = await _repository.RemoveAsync(id, cancellationToken);

        return !isDeleted ?
            Result<bool>.OnError(new NotFoundException(typeof(Course), id)) :
            Result<bool>.OnSuccess(true);
    }

    public async Task<Result<bool>> UpdateAsync(UpdateCourseBlockRequest request, CancellationToken cancellationToken)
    {
        var updatedCourse = CourseBlockRequestToModelMapping.MapToModel(request);
        var isUpdated = await _repository.UpdateAsync(updatedCourse, cancellationToken);

        return !isUpdated ?
            Result<bool>.OnError(new NotFoundException(typeof(CourseBlock), request.Id)) :
            Result<bool>.OnSuccess(true);
    }
}