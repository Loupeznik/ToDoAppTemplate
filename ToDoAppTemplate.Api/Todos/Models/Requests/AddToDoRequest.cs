using ToDoAppTemplate.Domain.Common;
using FastEndpoints;

namespace ToDoAppTemplate.Api.Todos.Models.Requests;

public sealed class AddToDoRequest : IAuthenticatedRequest
{
    [FromClaim]
    public int? UserId { get; set; }
    
    public string? Title { get; init; }
    
    public string? Description { get; init; }
}
