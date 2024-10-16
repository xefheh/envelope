using CoursesService.Application.Common;
using CoursesService.Application.Exceptions;
using CoursesService.Application.Mapping;
using CoursesService.Application.Repositories;
using CoursesService.Application.Requests.CoursTasks;
using CoursesService.Application.Services.Interfaces;
using CoursesService.Domain.Entities;

namespace CoursesService.Application.Services;

public class CourseTaskService: ICourseTaskService
{
    private readonly ICourseTaskRepository _repository;

    public CourseTaskService(ICourseTaskRepository repository) => _repository = repository;
    public async Task<Result<Guid>> AddAsync(AddCourseTaskRequest request, CancellationToken cancellationToken)
    {
        var task = CourseTaskRequestToModelMapping.MapToModel(request);
        var id = await _repository.AddAsync(task, cancellationToken);
        return Result<Guid>.OnSuccess(id);
    }

    public async Task<Result<bool>> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeleted = await _repository.RemoveAsync(id, cancellationToken);
        return !isDeleted
            ? Result<bool>.OnError(new NotFoundException(typeof(CourseTask), id))
            : Result<bool>.OnSuccess(true);
    }
}