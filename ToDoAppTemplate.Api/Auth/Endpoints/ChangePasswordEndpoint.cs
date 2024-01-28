using ToDoAppTemplate.Api.Infrastructure.Extensions;
using ToDoAppTemplate.Application.Auth.Commands;
using FastEndpoints;
using MediatR;
using ToDoAppTemplate.Api.Auth.Models;

namespace ToDoAppTemplate.Api.Auth.Endpoints;

public sealed class ChangePasswordEndpoint : Endpoint<PasswordChangeRequest>
{
    private readonly IMediator _mediator;

    public ChangePasswordEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Put(Common.Constants.Endpoints.ChangePassword);
        Description(x =>
                x.Accepts<PasswordChangeRequest>("application/json")
                 .Produces(204)
                 .ProducesProblemDetails(400, "application/json+problem")
                 .Produces(401),
            clearDefaults: true);
        Summary(x =>
            {
                x.Summary = "Changes the password for the currently logged in user.";
                x.Responses[204] = "Success.";
                x.Responses[400] = "Validation error, see Errors in response for details.";
                x.Responses[401] = "Authentication error.";
            }
        );
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
