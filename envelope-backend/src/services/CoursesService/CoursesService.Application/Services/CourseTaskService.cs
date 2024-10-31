using Envelope.Common.Exceptions;
using CoursesService.Application.Mapping;
using CoursesService.Application.Repositories;
using CoursesService.Application.Requests.CoursTasks;
using CoursesService.Application.Services.Interfaces;
using CoursesService.Domain.Entities;
using Envelope.Common.ResultPattern;

namespace CoursesService.Application.Services;

public class CourseTaskService: ICourseTaskService
{
    private readonly ICourseTaskRepository _repository;

    public CourseTaskService(ICourseTaskRepository repository) => _repository = repository;
    public async Task<Result<Guid>> AddAsync(AddCourseTaskRequest request, CancellationToken cancellationToken)
    {
        var task = CourseTaskRequestToModelMapping.MapToModel(request);
        var id = await _repository.AddAsync(task, cancellationToken);
        return id != default ? 
            Result<Guid>.OnSuccess(id) :
            Result<Guid>.OnFailure(new NotFoundException(typeof(CourseBlock), request.BlockId));
    }

    public async Task<Result<bool>> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeleted = await _repository.RemoveAsync(id, cancellationToken);
        return !isDeleted
            ? Result<bool>.OnFailure(new NotFoundException(typeof(CourseTask), id))
            : Result<bool>.OnSuccess(true);
    }
}