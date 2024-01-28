using ToDoAppTemplate.Domain.Common;
using FastEndpoints;

namespace ToDoAppTemplate.Api.Auth.Models;

public sealed class PasswordChangeRequest : IAuthenticatedRequest
{
    [FromClaim]
    public int? UserId { get; set; }

    public string OldPassword { get; init; } = string.Empty;

    public string NewPassword { get; init; } = string.Empty;
}
