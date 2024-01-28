namespace ToDoAppTemplate.Core.Infrastructure.Communication.Email.Smtp.Settings;

public sealed class SmtpConfiguration
{
    public string Host { get; init; } = string.Empty;
    
    public int Port { get; init; }
    
    public string Username { get; init; } = string.Empty;
    
    public string Password { get; init; } = string.Empty;
}
