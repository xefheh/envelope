namespace TaskService.Application.Common;

public class Result<T>
{
    public T? Value { get; init; }
    
    public Exception? Exception { get; init; }
    
    public bool IsSuccess => Exception == null;
    
    public static Result<T> OnSuccess(T value) =>
        new() { Value = value };
    
    public static Result<T> OnFailure(Exception exception) =>
        new() { Exception = exception };
}