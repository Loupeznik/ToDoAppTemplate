using DZarsky.ToDoAppTemplate.Api.Auth.Models;
using DZarsky.ToDoAppTemplate.Api.Infrastructure.Extensions;
using DZarsky.ToDoAppTemplate.Application.Auth.Commands;
using FastEndpoints;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Api.Auth.Endpoints;

public sealed class RequestPasswordResetEndpoint : Endpoint<RequestPasswordResetRequest>
{
    private readonly IMediator _mediator;

    public RequestPasswordResetEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Post(Common.Constants.Endpoints.RequestPasswordReset);
        AllowAnonymous();
    }

    public override async Task HandleAsync(RequestPasswordResetRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new RequestPasswordResetCommand(request.Login), ct);

        if (result.IsSuccess)
        {
            await this.SendEmptyResponse(204, ct); // consider sending 201 (resolve from result)
            return;
        }

        await this.ResolveResult(result, ct);
    }
}
