using TaskService.Application.Responses.TaskProjections.GetAllTaskProjections;
using TaskService.Application.Responses.TaskProjections.GetTaskProjection;
using TaskService.Domain.Projections;

namespace TaskService.Application.Mapping.Responses;

public class TaskResponseMapping
{
    public static GetAllTaskProjectionsInfo MapToProjectionInfo(TaskProjection projection) => new()
    {
        Id = projection.Id,
        Name = projection.Name,
        CreationDate = projection.CreationDate,
        UpdateDate = projection.UpdateDate,
        Difficult = projection.Difficult,
        State = projection.State
    };
    
    public static GetTaskProjectionResponse MapToTaskProjectionResponse(TaskProjection projection) => new()
    {        
        Id = projection.Id,
        Answer = projection.Answer,
        CreationDate = projection.CreationDate,
        Description = projection.Description,
        Difficult = projection.Difficult,
        ExecutionTime = projection.ExecutionTime,
        Name = projection.Name,
        UpdateDate = projection.UpdateDate,
        State = projection.State,
        AuthorId =projection.Author
    };
}