using MediatR;
using TaskService.Application.Common;

namespace TaskService.Application.Handlers.Commands.RemoveTask;

/// <summary>
/// Команда удаления 
/// </summary>
public class RemoveTaskCommand : IRequest<Result<Unit>>
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Автор
    /// </summary>
    public Guid Author { get; set; }
}