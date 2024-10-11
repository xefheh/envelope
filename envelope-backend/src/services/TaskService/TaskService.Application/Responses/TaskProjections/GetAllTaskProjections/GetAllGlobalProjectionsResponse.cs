namespace TaskService.Application.Responses.TaskProjections.GetAllTaskProjections;

public class GetAllTaskProjectionsResponse
{
    public ICollection<GetAllTaskProjectionsInfo> Response { get; set; } = [];
}