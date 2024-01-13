using DZarsky.ToDoAppTemplate.Api.Auth.Models;
using DZarsky.ToDoAppTemplate.Api.Infrastructure.Extensions;
using DZarsky.ToDoAppTemplate.Api.Infrastructure.Resolvers;
using DZarsky.ToDoAppTemplate.Application.Auth.Queries;
using DZarsky.ToDoAppTemplate.Core.Auth.Models;
using FastEndpoints;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Api.Auth.Endpoints;

public class LoginEndpoint : Endpoint<LoginRequest>
{
    private readonly IMediator _mediator;
    
    public LoginEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Post(Common.Constants.Endpoints.Login);
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LoginUserQuery(request.Login, request.Password), cancellationToken);

        if (result.Status != AuthResultStatus.Success)
        {
            await this.SendEmptyResponse(result.Status.Resolve(), cancellationToken);
            return;
        }
        
        // TODO: Generate token (add to extensions)
    }
}
