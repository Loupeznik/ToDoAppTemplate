using DZarsky.ToDoAppTemplate.Api.Common.Models;
using DZarsky.ToDoAppTemplate.Domain.Common;
using FastEndpoints;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Models.Requests;

public sealed class CompleteToDoRequest : GetResourceByIdRequest, IAuthenticatedRequest
{
    [FromClaim]
    public int? UserId { get; set; }
    
    public DateTime? CompletedDate { get; set; }
}
