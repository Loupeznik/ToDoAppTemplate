﻿using DZarsky.ToDoAppTemplate.Api.Auth.Models;
using DZarsky.ToDoAppTemplate.Api.Infrastructure.Extensions;
using DZarsky.ToDoAppTemplate.Application.Auth.Commands;
using FastEndpoints;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Api.Auth.Endpoints;

public sealed class ResetPasswordEndpoint : Endpoint<PasswordResetRequest>
{
    private readonly IMediator _mediator;

    public ResetPasswordEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Put(Common.Constants.Endpoints.ResetPassword);
        AllowAnonymous();
    }

    public override async Task HandleAsync(PasswordResetRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new ResetPasswordCommand(request.Login, request.Code, request.NewPassword),
            ct);

        if (result.IsSuccess)
        {
            await this.SendEmptyResponse(204, ct);
            return;
        }

        await this.ResolveResult(result, ct);
    }
}
