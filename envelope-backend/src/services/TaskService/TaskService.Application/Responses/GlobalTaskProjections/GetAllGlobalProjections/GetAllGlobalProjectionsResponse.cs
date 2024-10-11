namespace TaskService.Application.Responses.GlobalTaskProjections.GetAllGlobalProjections;

public class GetAllGlobalProjectionsResponse
{
    public ICollection<GetAllGlobalProjectionsInfo> Response { get; set; } = [];
}