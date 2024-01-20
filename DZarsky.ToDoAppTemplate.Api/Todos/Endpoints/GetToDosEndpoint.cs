using DZarsky.ToDoAppTemplate.Api.Infrastructure.Extensions;
using DZarsky.ToDoAppTemplate.Api.Todos.Models.Requests;
using DZarsky.ToDoAppTemplate.Api.Todos.Models.Responses;
using DZarsky.ToDoAppTemplate.Application.Todos.Queries;
using FastEndpoints;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Endpoints;

public sealed class GetToDosEndpoint : Endpoint<GetToDosRequest, GetToDosResponse>
{
    private readonly IMediator _mediator;

    public GetToDosEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(Common.Constants.Endpoints.Todos);
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
