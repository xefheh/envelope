namespace TaskService.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Type type, object key) : base($"{type.Name} with key {key} not found!") { }
}