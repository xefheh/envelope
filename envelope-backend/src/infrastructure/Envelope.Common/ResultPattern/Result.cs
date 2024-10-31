namespace Envelope.Common.ResultPattern;

/// <summary>
/// Результат выполнения действия
/// </summary>
/// <typeparam name="T">Тип возвращаемого результата</typeparam>
public class Result<T>
{
    /// <summary>
    /// Значение результата
    /// </summary>
    public T? Value { get; init; }

    /// <summary>
    /// Ошибка действия
    /// </summary>
    public Exception? Exception { get; init; }

    /// <summary>
    /// Проверка на удачное завершение действия
    /// </summary>
    public bool IsSuccess => Exception == null;

    /// <summary>
    /// Указание удачного завершения действия
    /// </summary>
    /// <param name="value">Значение результата</param>
    /// <returns>Результат</returns>
    public static Result<T> OnSuccess(T value) =>
        new() { Value = value };

    /// <summary>
    /// Указание неудачного завершения действия
    /// </summary>
    /// <param name="exception">Ошибка</param>
    /// <returns>Результат</returns>
    public static Result<T> OnFailure(Exception exception) =>
        new() { Exception = exception };
}