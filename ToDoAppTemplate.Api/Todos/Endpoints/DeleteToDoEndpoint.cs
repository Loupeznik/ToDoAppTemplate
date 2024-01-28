using ToDoAppTemplate.Api.Infrastructure.Extensions;
using ToDoAppTemplate.Application.Todos.Commands;
using FastEndpoints;
using MediatR;
using ToDoAppTemplate.Api.Todos.Models.Requests;

namespace ToDoAppTemplate.Api.Todos.Endpoints;

public sealed class DeleteToDoEndpoint : Endpoint<DeleteToDoRequest>
{
    private readonly IMediator _mediator;

    public DeleteToDoEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Delete(Common.Constants.Endpoints.DeleteOrUpdateTodo);
        Description(x =>
                x.Accepts<DeleteToDoRequest>("application/json")
                 .Produces(204)
                 .ProducesProblemDetails(400, "application/json+problem")
                 .Produces(401)
                 .Produces(404),
            clearDefaults: true);
        Summary(x =>
            {
                x.Summary = "Deletes a ToDo.";
                x.Responses[204] = "Success.";
                x.Responses[400] = "Validation error, see Errors in response for details.";
                x.Responses[401] = "Authentication error.";
                x.Responses[404] = "Entity not found.";
            }
        );
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
