namespace ToDoAppTemplate.Api.Auth.Models;

public sealed class RegisterRequest : LoginRequest
{
    public string? Email { get; set; }
}
