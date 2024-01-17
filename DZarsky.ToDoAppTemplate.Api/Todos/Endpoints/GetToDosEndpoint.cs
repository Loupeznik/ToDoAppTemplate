using DZarsky.ToDoAppTemplate.Api.Infrastructure.Security;
using DZarsky.ToDoAppTemplate.Api.Todos.Models.Requests;
using DZarsky.ToDoAppTemplate.Application.Todos.Queries;
using FastEndpoints;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Endpoints;

public sealed class GetToDosEndpoint : Endpoint<GetToDosRequest>
{
    private readonly IMediator _mediator;

    public GetToDosEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Get("todos");
    }
    
    public override async Task HandleAsync(GetToDosRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetToDosQuery(), cancellationToken);

        await SendAsync(result, cancellation: cancellationToken);
    }
}
