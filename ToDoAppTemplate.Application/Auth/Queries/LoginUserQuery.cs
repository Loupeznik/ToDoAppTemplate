using ToDoAppTemplate.Core.Auth.Models;
using ToDoAppTemplate.Core.Auth.Services;
using MediatR;

namespace ToDoAppTemplate.Application.Auth.Queries;

public sealed class LoginUserQuery : IRequest<AuthResult>
{
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public LoginUserQuery(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

public sealed class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, AuthResult>
{
    private readonly AuthManager _authManager;
    
    public LoginUserQueryHandler(AuthManager authManager) => _authManager = authManager;

    public async Task<AuthResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        // TODO: Generate token (fast endpoints)
        return await _authManager.ValidateCredentials(new Credentials(request.Username, request.Password));
    }
}
