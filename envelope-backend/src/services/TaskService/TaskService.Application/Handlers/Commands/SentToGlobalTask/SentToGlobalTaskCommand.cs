using MediatR;
using Envelope.Common.ResultPattern;

namespace TaskService.Application.Handlers.Commands.SentToGlobalTask;

public class SentToGlobalTaskCommand : IRequest<Result<Unit>>
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid Id { get; set; }
}