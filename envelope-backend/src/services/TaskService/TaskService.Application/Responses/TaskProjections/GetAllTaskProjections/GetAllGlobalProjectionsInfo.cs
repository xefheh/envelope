using Envelope.Common.Enums;

namespace TaskService.Application.Responses.TaskProjections.GetAllTaskProjections;

public class GetAllTaskProjectionsInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Difficult Difficult { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
    
    public TaskGlobalState State { get; set; }

    public string[] Tags { get; set; } = [];
}