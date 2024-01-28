using ToDoAppTemplate.Domain.Common;
using FastEndpoints;
using ToDoAppTemplate.Api.Common.Models;

namespace ToDoAppTemplate.Api.Todos.Models.Requests;

public sealed class DeleteToDoRequest : GetResourceByIdRequest, IAuthenticatedRequest
{
    [FromClaim]
    public int? UserId { get; set; }
}
