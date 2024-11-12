namespace Envelope.Common.Messages.RequestMessages.Tasks;

/// <summary>
/// Сообщение-запрос на получение информации об таске по Id
/// </summary>
public class GetTaskByIdRequestMessage
{
    /// <summary>
    /// Id задачи.
    /// </summary>
    public Guid Id { get; set; }
}