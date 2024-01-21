﻿using DZarsky.ToDoAppTemplate.Api.Infrastructure.Extensions;
using DZarsky.ToDoAppTemplate.Api.Todos.Models.Requests;
using DZarsky.ToDoAppTemplate.Api.Todos.Models.Responses;
using DZarsky.ToDoAppTemplate.Application.Todos.Commands;
using FastEndpoints;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Endpoints;

public sealed class AddToDoEndpoint : Endpoint<AddToDoRequest, GetToDoResponse>
{
    private readonly IMediator _mediator;

    public AddToDoEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Post(Common.Constants.Endpoints.Todos);
        Description(x =>
                x.Accepts<AddToDoRequest>("application/json")
                 .Produces<GetToDoResponse>(200, "application/json")
                 .ProducesProblemDetails(400, "application/json+problem")
                 .Produces(401),
            clearDefaults: true);
        Summary(x =>
            {
                x.Summary = "Adds a new ToDo.";
                x.Responses[200] = "Success.";
                x.Responses[400] = "Validation error, see Errors in response for details.";
                x.Responses[401] = "Authentication error.";
            }
        );
    }

    public override async Task HandleAsync(AddToDoRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AddToDoCommand(request.Title!, request.Description), cancellationToken);

        if (result is { IsSuccess: true, Result: not null })
        {
            var response = GetToDoResponse.MapFromToDo(result.Result);
            await this.ResolveResult(result, response, cancellationToken);
            return;
        }

        await this.ResolveResult(result, new GetToDoResponse(), cancellationToken);
    }
}
