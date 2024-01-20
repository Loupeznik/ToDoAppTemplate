using DZarsky.ToDoAppTemplate.Api.Infrastructure.Extensions;
using DZarsky.ToDoAppTemplate.Api.Todos.Models.Requests;
using DZarsky.ToDoAppTemplate.Api.Todos.Models.Responses;
using DZarsky.ToDoAppTemplate.Application.Todos.Commands;
using FastEndpoints;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Endpoints;

public sealed class UpdateToDoEndpoint : Endpoint<UpdateToDoRequest, GetToDoResponse>
{
    private readonly IMediator _mediator;

    public UpdateToDoEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put(Common.Constants.Endpoints.DeleteOrUpdateTodo);
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
