namespace TaskService.Application.Exceptions;

public class InvalidStateException : Exception
{
    public InvalidStateException(Type eventState) : base($"Invalid state on stream {eventState.Name}!") { }
}