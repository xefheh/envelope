using TaskService.Application.Responses.GlobalTaskProjections.GetAllGlobalProjections;

namespace TaskService.Application.Responses.SentToCheckProjections.GetAllSentToCheckProjections;

public class GetAllSentToCheckProjectionsResponse
{
    public ICollection<GetAllSentToCheckProjectionsInfo> Response { get; set; } = [];
}