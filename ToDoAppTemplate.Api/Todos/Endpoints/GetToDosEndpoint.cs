using ToDoAppTemplate.Api.Infrastructure.Extensions;
using ToDoAppTemplate.Application.Todos.Queries;
using FastEndpoints;
using MediatR;
using ToDoAppTemplate.Api.Todos.Models.Requests;
using ToDoAppTemplate.Api.Todos.Models.Responses;

namespace ToDoAppTemplate.Api.Todos.Endpoints;

public sealed class GetToDosEndpoint : Endpoint<GetToDosRequest, GetToDosResponse>
{
    private readonly IMediator _mediator;

    public GetToDosEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Get(Common.Constants.Endpoints.Todos);
        Description(x =>
                x.Accepts<GetToDosRequest>()
                 .Produces<GetToDosResponse>(200, "application/json")
                 .ProducesProblemDetails(400, "application/json+problem")
                 .Produces(401),
            clearDefaults: true);
        Summary(x =>
            {
                x.Summary = "Gets ToDos for the current user.";
                x.Responses[200] = "Success.";
                x.Responses[400] = "Validation error, see Errors in response for details.";
                x.Responses[401] = "Authentication error.";
            }
        );
    }

    public override async Task HandleAsync(GetToDosRequest request, CancellationToken cancellationToken)
    {
        var result =
            await _mediator.Send(
                new GetToDosQuery(request.IncludeDeleted, request.IncludeCompleted, request.Page, request.PageSize),
                cancellationToken);

        if (result is { IsSuccess: true, Result: not null })
        {
            var response = GetToDosResponse.MapFromQueryResult(result.Result);
            await this.ResolveResult(result, response, cancellationToken);
            return;
        }

        await this.ResolveResult(result, GetToDosResponse.GetEmptyResponse(), cancellationToken);
    }
}
