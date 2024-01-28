namespace ToDoAppTemplate.Api.Common.Constants;

public static class Endpoints
{
    private const string AuthPrefix = "/auth";

    public const string Register = $"{AuthPrefix}/register";
    public const string ResetPassword = $"{AuthPrefix}/password-reset/reset";
    public const string RequestPasswordReset = $"{AuthPrefix}/password-reset/request";
    public const string Login = $"{AuthPrefix}/login";
    public const string ChangePassword = $"{AuthPrefix}/change-password";

    public const string Todos = "/todos";
    public const string DeleteOrUpdateTodo = $"{Todos}/{{id}}";
    public const string CompleteToDo = $"{Todos}/{{id}}/complete";
}
