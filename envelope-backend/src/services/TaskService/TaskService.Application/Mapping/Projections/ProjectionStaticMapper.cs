using TaskService.Domain.Projections;

namespace TaskService.Application.Mapping.Projections;

public class ProjectionStaticMapper
{
    public static GlobalTaskProjection MapToGlobalProjection(Domain.Aggregates.Task aggregate) => new()
    {
        Id = aggregate.Id,
        Answer = aggregate.Answer,
        Author = aggregate.Author,
        CreationDate = aggregate.CreationDate,
        Description = aggregate.Description,
        Difficult = aggregate.Difficult,
        ExecutionTime = aggregate.ExecutionTime,
        Name = aggregate.Name,
        UpdateDate = aggregate.UpdateDate
    };

    public static TaskProjection MapToTaskProjection(Domain.Aggregates.Task aggregate) => new()
    {
        Id = aggregate.Id,
        Answer = aggregate.Answer,
        Author = aggregate.Author,
        CreationDate = aggregate.CreationDate,
        Description = aggregate.Description,
        Difficult = aggregate.Difficult,
        ExecutionTime = aggregate.ExecutionTime,
        Name = aggregate.Name,
        State = aggregate.State,
        UpdateDate = aggregate.UpdateDate
    };
    
    public static SentToCheckTaskProjection MapToSentToCheckTaskProjection(Domain.Aggregates.Task aggregate) => new()
    {
        Id = aggregate.Id,
        Answer = aggregate.Answer,
        Author = aggregate.Author,
        Description = aggregate.Description,
        Difficult = aggregate.Difficult,
        ExecutionTime = aggregate.ExecutionTime,
        Name = aggregate.Name
    };
}