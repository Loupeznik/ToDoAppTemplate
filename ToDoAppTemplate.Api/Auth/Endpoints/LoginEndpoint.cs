using ToDoAppTemplate.Api.Infrastructure.Extensions;
using ToDoAppTemplate.Api.Infrastructure.Resolvers;
using ToDoAppTemplate.Application.Auth.Queries;
using ToDoAppTemplate.Core.Auth.Models;
using FastEndpoints;
using MediatR;
using ToDoAppTemplate.Api.Auth.Models;
using ToDoAppTemplate.Api.Infrastructure.Security;

namespace ToDoAppTemplate.Api.Auth.Endpoints;

public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly IMediator _mediator;
    private readonly TokenGenerator _tokenGenerator;

    public LoginEndpoint(IMediator mediator, TokenGenerator tokenGenerator)
    {
        _mediator = mediator;
        _tokenGenerator = tokenGenerator;
    }

    public override void Configure()
    {
        Post(Common.Constants.Endpoints.Login);
        AllowAnonymous();
        Description(x =>
                x.Accepts<LoginRequest>("application/json")
                 .Produces<LoginResponse>(200, "application/json")
                 .ProducesProblemDetails(400, "application/json+problem")
                 .Produces(401),
            clearDefaults: true);
        Summary(x =>
            {
                x.Summary = "Provides an access token.";
                x.Responses[200] = "Success.";
                x.Responses[400] = "Validation error, see Errors in response for details.";
                x.Responses[401] = "Invalid credentials.";
            }
        );
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LoginUserQuery(request.Login, request.Password), cancellationToken);

        if (result.Status != AuthResultStatus.Success)
        {
            await this.SendEmptyResponse(result.Status.Resolve(), cancellationToken);
            return;
        }

        await SendAsync(new LoginResponse(_tokenGenerator.GenerateToken(result, request.Login)),
            cancellation: cancellationToken);
    }
}
