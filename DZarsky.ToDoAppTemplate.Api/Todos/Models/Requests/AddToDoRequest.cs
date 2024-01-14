using DZarsky.ToDoAppTemplate.Api.Common.Interfaces;
using FastEndpoints;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Models.Requests;

public sealed class AddToDoRequest : IAuthenticatedRequest
{
    [FromClaim]
    public int? UserId { get; set; }
    
    public string? Title { get; init; }
    
    public string? Description { get; init; }
}
