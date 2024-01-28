using ToDoAppTemplate.Core.Infrastructure.Security;
using ToDoAppTemplate.Data.Infrastructure.EF;
using ToDoAppTemplate.Domain.Common.Errors;
using ToDoAppTemplate.Domain.Common.MediatR;
using ToDoAppTemplate.Domain.Common.Results;
using ToDoAppTemplate.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ToDoAppTemplate.Application.Auth.Commands;

public sealed class ResetPasswordCommand : IRequest<MediatrBaseResult>
{
    public string Login { get; set; }

    public string Code { get; set; }

    public string NewPassword { get; set; }

    public ResetPasswordCommand(string login, string code, string newPassword)
    {
        Login = login;
        Code = code;
        NewPassword = newPassword;
    }
}

public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, MediatrBaseResult>
{
    private readonly DataContext _dataContext;
    private readonly PasswordHasher _passwordHasher;

    public ResetPasswordCommandHandler(DataContext dataContext, PasswordHasher hasher)
    {
        _dataContext = dataContext;
        _passwordHasher = hasher;
    }

    public async Task<MediatrBaseResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _dataContext
                         .Set<User>()
                         .Include(x => x.PasswordResetCodes)
                         .FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken);

        if (user == null)
        {
            return new MediatrBaseResult(ResultStatus.EntityNotFound);
        }

        var code = user.PasswordResetCodes.FirstOrDefault(x => x.Code == request.Code);

        if (code == null)
        {
            return new MediatrBaseResult(ResultStatus.ValidationError, new List<ErrorDescription>
            {
                new("Code.Invalid", "The password reset code does not match any available code.")
            });
        }

        if (code.ExpirationDate <= DateTime.UtcNow)
        {
            return new MediatrBaseResult(ResultStatus.ValidationError, new List<ErrorDescription>
            {
                new("Code.Expired", "The password reset code has expired.")
            });
        }

        if (code.IsUsed)
        {
            return new MediatrBaseResult(ResultStatus.ValidationError, new List<ErrorDescription>
            {
                new("Code.Used", "The password reset code has already been used.")
            });
        }

        user.Password = _passwordHasher.HashPassword(request.NewPassword);
        code.IsUsed = true;

        _dataContext.Update(user);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return new MediatrBaseResult(ResultStatus.Success);
    }
}
