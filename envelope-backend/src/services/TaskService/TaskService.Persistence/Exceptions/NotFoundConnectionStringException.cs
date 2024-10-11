namespace TaskService.Persistence.Exceptions;

public class NotFoundConnectionStringException : Exception
{
    public NotFoundConnectionStringException(string connectionName) : base($"{connectionName} not found") { }
}