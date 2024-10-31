namespace Envelope.Common.Exceptions;

/// <summary>
/// Исключение не найденной сущности
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Конструктор исключения
    /// </summary>
    /// <param name="type">Тип не найденной сущности</param>
    /// <param name="key">Суррогатный ключ</param>
    public NotFoundException(Type type, object key) : base($"{type.Name} with key {key} not found!") { }
}