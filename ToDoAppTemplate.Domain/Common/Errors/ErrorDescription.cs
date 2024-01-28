namespace ToDoAppTemplate.Domain.Common.Errors;

public sealed record ErrorDescription(string Key, string? Message = null);
