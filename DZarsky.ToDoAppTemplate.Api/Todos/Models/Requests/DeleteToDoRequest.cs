using DZarsky.ToDoAppTemplate.Api.Common.Interfaces;
using DZarsky.ToDoAppTemplate.Api.Common.Models;
using FastEndpoints;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Models.Requests;

public sealed class DeleteToDoRequest : GetResourceByIdRequest, IAuthenticatedRequest
{
    [FromClaim]
    public int? UserId { get; set; }
}
