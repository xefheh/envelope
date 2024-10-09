namespace TaskService.Persistence.Factories.Options;

public class ConnectionConfiguration
{
    public string Database { get; set; } = null!;
    public string Table { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Server { get; set; } = null!;
    public string Port { get; set; } = null!;
}