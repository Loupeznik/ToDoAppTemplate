using ToDoAppTemplate.Domain.Common;
using FastEndpoints;
using ToDoAppTemplate.Api.Common.Models;

namespace ToDoAppTemplate.Api.Todos.Models.Requests;

public sealed class CompleteToDoRequest : GetResourceByIdRequest, IAuthenticatedRequest
{
    [FromClaim]
    public int? UserId { get; set; }
    
    public DateTime? CompletedDate { get; set; }
}
