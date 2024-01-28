namespace ToDoAppTemplate.Api.Auth.Models;

public sealed class LoginResponse
{
    public string Token { get; init; }
    
    // TODO: Add refresh token support
    
    public LoginResponse(string token) => Token = token;
}
