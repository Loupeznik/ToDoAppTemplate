using ToDoAppTemplate.Api.Infrastructure.Extensions;
using ToDoAppTemplate.Api.Infrastructure.Resolvers;
using ToDoAppTemplate.Application.Auth.Commands;
using FastEndpoints;
using MediatR;
using ToDoAppTemplate.Api.Auth.Models;

namespace ToDoAppTemplate.Api.Auth.Endpoints;

public sealed class RegisterEndpoint : Endpoint<RegisterRequest>
{
    private readonly IMediator _mediator;

    public RegisterEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Post(Common.Constants.Endpoints.Register);
        AllowAnonymous();
        Description(x =>
                x.Accepts<RegisterRequest>("application/json")
                 .Produces(201)
                 .ProducesProblemDetails(400, "application/json+problem")
                 .Produces(409),
            clearDefaults: true);
        Summary(x =>
            {
                x.Summary = "Registers a new user.";
                x.Responses[201] = "Success.";
                x.Responses[400] = "Validation error, see Errors in response for details.";
                x.Responses[409] = "Conflict.";
            }
        );
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RegisterUserCommand(request.Login, request.Password, request.Email),
            cancellationToken);

        if (result.IsSuccess)
        {
            await this.SendEmptyResponse(result.Status.Resolve(), cancellationToken);
            return;
        }

        await this.ResolveResult(result, cancellationToken);
    }
}
