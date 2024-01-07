using System.Text.Json.Serialization;

namespace DZarsky.ToDoAppTemplate.Core.Auth.Models;

public sealed record AuthResult(AuthResultStatus Status, int? UserId = null);

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AuthResultStatus
{
    Success,
    InvalidLoginOrPassword,
    UserInactive,
    Error
}
