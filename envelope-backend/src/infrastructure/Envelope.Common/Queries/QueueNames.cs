namespace Envelope.Common.Queries;

/// <summary>
/// Класс с константами названий очередей
/// </summary>
public class QueueNames
{
    /// <summary>
    /// Очередь на получение информации о задачах
    /// </summary>
    public const string GetTaskQueue = "GET_TASK_QUERY";

    /// <summary>
    /// Очередь на получение информации о пользователях
    /// </summary>
    public const string GetUserQueue = "GET_USER_QUERY";

    public const string AddTagQueue = "ADD_TAG_QUEUE";

    public const string GetTagQueue = "GET_TAG_QUEUE";
}