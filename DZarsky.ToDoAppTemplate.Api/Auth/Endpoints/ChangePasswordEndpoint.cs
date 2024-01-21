using DZarsky.ToDoAppTemplate.Api.Auth.Models;
using DZarsky.ToDoAppTemplate.Api.Infrastructure.Extensions;
using DZarsky.ToDoAppTemplate.Application.Auth.Commands;
using FastEndpoints;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Api.Auth.Endpoints;

public sealed class ChangePasswordEndpoint : Endpoint<PasswordChangeRequest>
{
    private readonly IMediator _mediator;

    public ChangePasswordEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Put(Common.Constants.Endpoints.ChangePassword);
    }

    public override async Task HandleAsync(PasswordChangeRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new ChangePasswordCommand(request.OldPassword, request.NewPassword), ct);

        if (result.IsSuccess)
        {
            await this.SendEmptyResponse(204, ct);
            return;
        }

        await this.ResolveResult(result, ct);
    }
}
