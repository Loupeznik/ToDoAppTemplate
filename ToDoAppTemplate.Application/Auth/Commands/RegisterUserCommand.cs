using System.Text.Json;
using ToDoAppTemplate.Core.Infrastructure.Security;
using ToDoAppTemplate.Data.Infrastructure.EF;
using ToDoAppTemplate.Domain.Common.Errors;
using ToDoAppTemplate.Domain.Common.MediatR;
using ToDoAppTemplate.Domain.Common.Results;
using ToDoAppTemplate.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ToDoAppTemplate.Application.Auth.Commands;

public sealed class RegisterUserCommand : IRequest<MediatrBaseResult>
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string? Email { get; set; }

    public RegisterUserCommand(string username, string password, string? email)
    {
        Username = username;
        Password = password;
        Email = email;
    }
}

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, MediatrBaseResult>
{
    private readonly DataContext _dataContext;
    private readonly ILogger<RegisterUserCommandHandler> _logger;
    private readonly PasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(DataContext dataContext, ILogger<RegisterUserCommandHandler> logger,
        PasswordHasher hasher)
    {
        _dataContext = dataContext;
        _logger = logger;
        _passwordHasher = hasher;
    }

    public async Task<MediatrBaseResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var loginExists = await _dataContext
                                .Set<User>()
                                .AnyAsync(x => x.Login == request.Username || x.Email == request.Email,
                                    cancellationToken);

        if (loginExists)
        {
            return new MediatrBaseResult(ResultStatus.AlreadyExists);
        }

        var user = new User
        {
            Login = request.Username,
            Password = _passwordHasher.HashPassword(request.Password),
            Email = request.Email
        };

        try
        {
            await _dataContext.Set<User>().AddAsync(user, cancellationToken);

            await _dataContext.SaveChangesAsync(cancellationToken);
            
            // TODO: Send email?

            return new MediatrBaseResult(ResultStatus.EntityCreated);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while saving new user: {Message}. Entity to save: {User}", e.Message,
                JsonSerializer.Serialize(user));

            return new MediatrBaseResult(ResultStatus.InternalError, new List<ErrorDescription>
            {
                new("SavingEntity", e.Message)
            });
        }
    }
}
