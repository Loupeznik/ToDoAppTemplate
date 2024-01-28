using ToDoAppTemplate.Api.Infrastructure.Extensions;
using ToDoAppTemplate.Application.Auth.Commands;
using FastEndpoints;
using MediatR;
using ToDoAppTemplate.Api.Auth.Models;

namespace ToDoAppTemplate.Api.Auth.Endpoints;

public sealed class ResetPasswordEndpoint : Endpoint<PasswordResetRequest>
{
    private readonly IMediator _mediator;

    public ResetPasswordEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Put(Common.Constants.Endpoints.ResetPassword);
        AllowAnonymous();
        Description(x =>
                x.Accepts<PasswordResetRequest>("application/json")
                 .Produces(204)
                 .ProducesProblemDetails(400, "application/json+problem"),
            clearDefaults: true);
        Summary(x =>
            {
                x.Summary = "Resets a user's password.";
                x.Responses[204] = "Success.";
                x.Responses[400] = "Validation error, see Errors in response for details.";
            }
        );
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
