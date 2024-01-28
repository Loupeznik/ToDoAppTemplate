namespace ToDoAppTemplate.Api.Auth.Models;

public class PasswordResetRequest : RequestPasswordResetRequest
{
    public string Code { get; init; } = string.Empty;

    public string NewPassword { get; init; } = string.Empty;
}
