using ToDoAppTemplate.Api.Infrastructure.Extensions;
using ToDoAppTemplate.Application.Todos.Commands;
using FastEndpoints;
using MediatR;
using ToDoAppTemplate.Api.Todos.Models.Requests;
using ToDoAppTemplate.Api.Todos.Models.Responses;

namespace ToDoAppTemplate.Api.Todos.Endpoints;

public sealed class CompleteToDoEndpoint : Endpoint<CompleteToDoRequest, GetToDoResponse>
{
    private readonly IMediator _mediator;

    public CompleteToDoEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Put(Common.Constants.Endpoints.CompleteToDo);
        Description(x =>
                x.Accepts<CompleteToDoRequest>("application/json")
                 .Produces<GetToDoResponse>(200, "application/json")
                 .ProducesProblemDetails(400, "application/json+problem")
                 .Produces(401)
                 .Produces(404),
            clearDefaults: true);
        Summary(x =>
            {
                x.Summary = "Marks ToDo as complete.";
                x.Responses[200] = "Success.";
                x.Responses[400] = "Validation error, see Errors in response for details.";
                x.Responses[401] = "Authentication error.";
                x.Responses[404] = "Entity not found.";
            }
        );
    }

    public override async Task HandleAsync(CompleteToDoRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CompleteToDoCommand(request.Id), cancellationToken);

        if (result is { IsSuccess: true, Result: not null })
        {
            var response = GetToDoResponse.MapFromToDo(result.Result);
            await this.ResolveResult(result, response, cancellationToken);
            return;
        }

        await this.ResolveResult(result, new GetToDoResponse(), cancellationToken);
    }
}
