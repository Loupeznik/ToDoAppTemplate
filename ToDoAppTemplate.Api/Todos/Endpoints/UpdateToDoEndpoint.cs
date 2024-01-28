using ToDoAppTemplate.Api.Infrastructure.Extensions;
using ToDoAppTemplate.Application.Todos.Commands;
using FastEndpoints;
using MediatR;
using ToDoAppTemplate.Api.Todos.Models.Requests;
using ToDoAppTemplate.Api.Todos.Models.Responses;

namespace ToDoAppTemplate.Api.Todos.Endpoints;

public sealed class UpdateToDoEndpoint : Endpoint<UpdateToDoRequest, GetToDoResponse>
{
    private readonly IMediator _mediator;

    public UpdateToDoEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Put(Common.Constants.Endpoints.DeleteOrUpdateTodo);
        Description(x =>
                x.Accepts<CompleteToDoRequest>("application/json")
                 .Produces<GetToDoResponse>(200, "application/json")
                 .ProducesProblemDetails(400, "application/json+problem")
                 .Produces(401)
                 .Produces(404),
            clearDefaults: true);
        Summary(x =>
            {
                x.Summary = "Updates a ToDo.";
                x.Responses[200] = "Success.";
                x.Responses[400] = "Validation error, see Errors in response for details.";
                x.Responses[401] = "Authentication error.";
                x.Responses[404] = "Entity not found.";
            }
        );
    }

    public override async Task HandleAsync(UpdateToDoRequest request, CancellationToken cancellationToken)
    {
        var result =
            await _mediator.Send(
                new UpdateToDoCommand(request.Id, request.Title!, request.Description, request.IsComplete,
                    request.DateCompleted), cancellationToken);

        if (result is { IsSuccess: true, Result: not null })
        {
            var response = GetToDoResponse.MapFromToDo(result.Result);
            await this.ResolveResult(result, response, cancellationToken);
            return;
        }

        await this.ResolveResult(result, new GetToDoResponse(), cancellationToken);
    }
}
