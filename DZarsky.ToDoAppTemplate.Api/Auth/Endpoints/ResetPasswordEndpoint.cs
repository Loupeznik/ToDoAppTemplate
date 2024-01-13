using DZarsky.ToDoAppTemplate.Api.Auth.Models;
using DZarsky.ToDoAppTemplate.Api.Infrastructure.Extensions;
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
    }
    
    public override async Task HandleAsync(PasswordResetRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
