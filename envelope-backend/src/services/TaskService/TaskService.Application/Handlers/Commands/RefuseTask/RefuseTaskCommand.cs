﻿using MediatR;
using TaskService.Application.Common;

namespace TaskService.Application.Handlers.Commands.RefuseTask;

public class RefuseTaskCommand : IRequest<Result<Unit>>
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid Id { get; set; }
}