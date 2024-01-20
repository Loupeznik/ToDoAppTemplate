using DZarsky.ToDoAppTemplate.Api.Infrastructure.Extensions;
using DZarsky.ToDoAppTemplate.Api.Todos.Models.Requests;
using DZarsky.ToDoAppTemplate.Application.Todos.Commands;
using FastEndpoints;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Endpoints;

public sealed class DeleteToDoEndpoint : Endpoint<DeleteToDoRequest>
{
    private readonly IMediator _mediator;

    public DeleteToDoEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete(Common.Constants.Endpoints.DeleteOrUpdateTodo);
    }

    public override async Task HandleAsync(DeleteToDoRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteToDoCommand(request.Id), cancellationToken);

        if (result.IsSuccess)
        {
            await this.SendEmptyResponse(204, cancellationToken);
            return;
        }

        await this.ResolveResult(result, cancellationToken);
    }
}
