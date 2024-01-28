using ToDoAppTemplate.Domain.Common;
using ToDoAppTemplate.Domain.Todos;

namespace ToDoAppTemplate.Domain.Users;

public sealed class User : BaseEntity
{
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? Email { get; set; }

    public bool IsBlocked { get; set; }

    public IList<Todo> Todos { get; set; } = new List<Todo>();

    public IList<PasswordResetCode> PasswordResetCodes { get; set; } = new List<PasswordResetCode>();
}
