using Envelope.Common.Enums;

namespace TaskService.Application.Responses.GlobalTaskProjections.GetAllGlobalProjections;

public class GetAllGlobalProjectionsInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Difficult Difficult { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
}