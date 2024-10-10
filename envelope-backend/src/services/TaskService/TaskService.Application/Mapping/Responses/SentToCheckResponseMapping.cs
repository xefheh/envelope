using TaskService.Application.Responses.SentToCheckProjections.GetAllSentToCheckProjections;
using TaskService.Application.Responses.SentToCheckProjections.GetSentToCheckProjection;
using TaskService.Domain.Projections;

namespace TaskService.Application.Mapping.Responses;

public class SentToCheckResponseMapping
{
    public static GetAllSentToCheckProjectionsInfo MapToProjectionInfo(SentToCheckTaskProjection projection) => new()
    {
        Id = projection.Id,
        Name = projection.Name,
        Difficult = projection.Difficult
    };
    
    public static GetSentToCheckProjectionResponse MapToSentToCheckResponse(SentToCheckTaskProjection projection) => new()
    {        
        Id = projection.Id,
        Answer = projection.Answer,
        Author = projection.Author,
        Description = projection.Description,
        Difficult = projection.Difficult,
        ExecutionTime = projection.ExecutionTime,
        Name = projection.Name
    };
}