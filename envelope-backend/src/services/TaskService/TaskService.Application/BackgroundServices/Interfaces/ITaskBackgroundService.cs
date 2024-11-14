using Envelope.Common.Messages.RequestMessages.Tasks;
using Envelope.Common.Messages.ResponseMessages.Tasks;

namespace TaskService.Application.BackgroundServices.Interfaces;

/// <summary>
/// Background сервис задач
/// </summary>
public interface ITaskBackgroundService
{
    /// <summary>
    /// Получить ответ на запрос получения задачи по Id
    /// </summary>
    /// <param name="message">Сообщение-запрос на получение</param>
    /// <returns>Сообщение-ответ с информацией</returns>
    Task<TaskResponseMessage> ResponseAsync(GetTaskByIdRequestMessage message);
}