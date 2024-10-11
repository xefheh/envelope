using TaskService.Domain.Enums;

namespace TaskService.Application.Responses.SentToCheckProjections.GetAllSentToCheckProjections;

public class GetAllSentToCheckProjectionsInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Difficult Difficult { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
}