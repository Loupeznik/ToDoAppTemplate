using FastEndpoints;

namespace DZarsky.ToDoAppTemplate.Api.Common.Interfaces;

public interface IAuthenticatedRequest
{
    [FromClaim]
    public int? UserId { get; set; }
}
