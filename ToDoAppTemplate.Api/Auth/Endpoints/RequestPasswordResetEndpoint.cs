using ToDoAppTemplate.Api.Infrastructure.Extensions;
using ToDoAppTemplate.Application.Auth.Commands;
using FastEndpoints;
using MediatR;
using ToDoAppTemplate.Api.Auth.Models;

namespace ToDoAppTemplate.Api.Auth.Endpoints;

public sealed class RequestPasswordResetEndpoint : Endpoint<RequestPasswordResetRequest>
{
    private readonly IMediator _mediator;

    public RequestPasswordResetEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Post(Common.Constants.Endpoints.RequestPasswordReset);
        AllowAnonymous();
        Description(x =>
                x.Accepts<RequestPasswordResetRequest>("application/json")
                 .Produces(204)
                 .ProducesProblemDetails(400, "application/json+problem"),
            clearDefaults: true);
        Summary(x =>
            {
                x.Summary = "Requests a new password reset code.";
                x.Responses[204] = "Success.";
                x.Responses[400] = "Validation error, see Errors in response for details.";
            }
        );
    }

    public override async Task HandleAsync(RequestPasswordResetRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new RequestPasswordResetCommand(request.Login), ct);
        // consider sending 202 and processing in the background
        
        if (result.IsSuccess)
        {
            await this.SendEmptyResponse(204, ct);
            return;
        }

        await this.ResolveResult(result, ct);
    }
}
