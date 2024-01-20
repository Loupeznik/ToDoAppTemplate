namespace DZarsky.ToDoAppTemplate.Api.Common.Constants;

public static class Endpoints
{
    private const string AuthPrefix = "/auth";

    public const string Register = $"{AuthPrefix}/register";
    public const string ResetPassword = $"{AuthPrefix}/reset-password";
    public const string Login = $"{AuthPrefix}/login";

    public const string Todos = "/todos";
    public const string DeleteOrUpdateTodo = $"{Todos}/{{id}}";
}
