using DZarsky.ToDoAppTemplate.Api.Auth.Models;
using DZarsky.ToDoAppTemplate.Api.Infrastructure.Extensions;
using DZarsky.ToDoAppTemplate.Api.Infrastructure.Resolvers;
using DZarsky.ToDoAppTemplate.Application.Auth.Commands;
using FastEndpoints;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Api.Auth.Endpoints;

public sealed class RegisterEndpoint : Endpoint<RegisterRequest>
{
    private readonly IMediator _mediator;

    public RegisterEndpoint(IMediator mediator) => _mediator = mediator;
    
    public override void Configure()
    {
        Post(Common.Constants.Endpoints.Register);
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RegisterUserCommand(request.Login, request.Password, request.Email), cancellationToken);

        // TODO: Validation, implement returning errors from command
        
        await this.SendEmptyResponse(result.Status.Resolve(), cancellationToken);
    }
}
