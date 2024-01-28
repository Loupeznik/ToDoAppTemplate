using ToDoAppTemplate.Domain.Common;
using ToDoAppTemplate.Domain.Common.Requests;
using FastEndpoints;

namespace ToDoAppTemplate.Api.Todos.Models.Requests;

public sealed class GetToDosRequest : PagedRequest, IAuthenticatedRequest
{
    [FromClaim]
    public int? UserId { get; set; }

    public bool IncludeCompleted { get; init; }

    public bool IncludeDeleted { get; init; }
}
