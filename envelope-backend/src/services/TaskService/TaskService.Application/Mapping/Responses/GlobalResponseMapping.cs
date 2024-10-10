using TaskService.Application.Responses.GlobalTaskProjections.GetAllGlobalProjections;
using TaskService.Application.Responses.GlobalTaskProjections.GetGlobalProjection;
using TaskService.Domain.Projections;

namespace TaskService.Application.Mapping.Responses;

public class GlobalResponseMapping
{
    public static GetAllGlobalProjectionsInfo MapToProjectionInfo(GlobalTaskProjection projection) => new()
    {
        Id = projection.Id,
        Name = projection.Name,
        CreationDate = projection.CreationDate,
        UpdateDate = projection.UpdateDate,
        Difficult = projection.Difficult
    };
    
    public static GetGlobalProjectionResponse MapToGlobalResponse(GlobalTaskProjection projection) => new()
    {        
        Id = projection.Id,
        Answer = projection.Answer,
        Author = projection.Author,
        CreationDate = projection.CreationDate,
        Description = projection.Description,
        Difficult = projection.Difficult,
        ExecutionTime = projection.ExecutionTime,
        Name = projection.Name,
        UpdateDate = projection.UpdateDate
    };
}