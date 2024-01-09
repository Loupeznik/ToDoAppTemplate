﻿using DZarsky.ToDoAppTemplate.Core.Auth.Models;
using DZarsky.ToDoAppTemplate.Core.Infrastructure.Security;
using DZarsky.ToDoAppTemplate.Data.Infrastructure.EF;
using DZarsky.ToDoAppTemplate.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DZarsky.ToDoAppTemplate.Core.Auth.Services;

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

        return new AuthResult(AuthResultStatus.Success, user.Id);
    }
}