using ToDoAppTemplate.Data.Infrastructure.EF;
using ToDoAppTemplate.Domain.Users;
using Microsoft.EntityFrameworkCore;
using ToDoAppTemplate.Core.Auth.Models;
using ToDoAppTemplate.Core.Infrastructure.Security;

namespace ToDoAppTemplate.Core.Auth.Services;

public sealed class AuthManager
{
    private readonly DataContext _dataContext;
    private readonly PasswordValidator _validator;

    public AuthManager(DataContext dataContext, PasswordValidator validator)
    {
        _dataContext = dataContext;
        _validator = validator;
    }

    public async Task<AuthResult> ValidateCredentials(Credentials credentials)
    {
        if (string.IsNullOrWhiteSpace(credentials.Login) || string.IsNullOrWhiteSpace(credentials.Password))
        {
            return new AuthResult(AuthResultStatus.InvalidLoginOrPassword);
        }

        var user = await _dataContext.Set<User>()
                                      .FirstOrDefaultAsync(u => u.Login == credentials.Login);

        if (user == null || !_validator.ValidatePassword(credentials.Password, user.Password))
        {
            return new AuthResult(AuthResultStatus.InvalidLoginOrPassword);
        }

        return new AuthResult(user.IsBlocked ? AuthResultStatus.UserInactive : AuthResultStatus.Success, user.Id);
    }
}
