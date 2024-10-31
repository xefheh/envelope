﻿using MediatR;
using Envelope.Common.ResultPattern;

namespace TaskService.Application.Handlers.Commands.SentToCheckTask;

public class SentToCheckTaskCommand : IRequest<Result<Unit>>
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid Id { get; set; }
}