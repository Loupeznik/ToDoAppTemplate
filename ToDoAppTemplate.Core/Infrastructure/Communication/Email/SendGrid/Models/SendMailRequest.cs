using System.Text.Json.Serialization;

namespace ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid.Models;

public sealed record SendMailRequest
{
    [JsonPropertyName("personalizations")]
    public IList<Personalization> Personalizations { get; set; } = new List<Personalization>();

    [JsonPropertyName("from")]
    public EmailAddress? From { get; set; }

    [JsonPropertyName("content")]
    public IList<Content> Content { get; set; } = new List<Content>();
}

public sealed record Personalization
{
    [JsonPropertyName("to")]
    public IEnumerable<EmailAddress> To { get; set; } = new List<EmailAddress>();

    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;
}

public sealed record EmailAddress
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public sealed record Content
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
