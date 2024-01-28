namespace ToDoAppTemplate.Core.Infrastructure.Security.Models;

public sealed record SecurityConfiguration
{
    public string ArgonSecret { get; init; } = string.Empty;
}
