namespace Envelope.Integration.Options;

public class RabbitMqOptions
{
    public string Hostname { get; set; } = null!;
    public int Port { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}