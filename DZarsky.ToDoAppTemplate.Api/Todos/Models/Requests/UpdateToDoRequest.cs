using DZarsky.ToDoAppTemplate.Api.Common.Models;
using DZarsky.ToDoAppTemplate.Domain.Common;
using FastEndpoints;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Models.Requests;

public sealed class UpdateToDoRequest : GetResourceByIdRequest, IAuthenticatedRequest
{
    [FromClaim]
    public int? UserId { get; set; }
    
    public string? Title { get; init; }
    
    public string? Description { get; init; }
    
    public bool? IsComplete { get; init; }
    
    public DateTime? DateCompleted { get; init; }
}
