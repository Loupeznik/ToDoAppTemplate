namespace ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid.Settings;

public sealed class SendGridConfiguration
{
    public string ApiKey { get; init; } = string.Empty;

    public string BaseUrl { get; init; } = string.Empty;

    public string SenderEmail { get; init; } = string.Empty;
}
