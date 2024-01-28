using ToDoAppTemplate.Core.Infrastructure.Security;
using ToDoAppTemplate.Data.Infrastructure.EF;
using ToDoAppTemplate.Domain.Common;
using ToDoAppTemplate.Domain.Common.Errors;
using ToDoAppTemplate.Domain.Common.MediatR;
using ToDoAppTemplate.Domain.Common.Results;
using ToDoAppTemplate.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ToDoAppTemplate.Application.Auth.Commands;

public sealed class ChangePasswordCommand : IRequest<MediatrBaseResult>, IAuthenticatedRequest
{
    public int? UserId { get; set; }

    public string OldPassword { get; init; }

    public string NewPassword { get; init; }


    public ChangePasswordCommand(string oldPassword, string newPassword)
    {
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }
}

public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, MediatrBaseResult>
{
    private readonly DataContext _dataContext;
    private readonly PasswordHasher _passwordHasher;

    public ChangePasswordCommandHandler(DataContext dataContext, PasswordHasher hasher)
    {
        _dataContext = dataContext;
        _passwordHasher = hasher;
    }

    public async Task<MediatrBaseResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _dataContext
                         .Set<User>()
                         .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return new MediatrBaseResult(ResultStatus.EntityNotFound);
        }

        if (_passwordHasher.HashPassword(request.OldPassword) != user.Password)
        {
            return new MediatrBaseResult(ResultStatus.ValidationError, new List<ErrorDescription>()
            {
                new(nameof(request.OldPassword), "Old password does not match the current password.")
            });
        }

        user.Password = _passwordHasher.HashPassword(request.NewPassword);

        _dataContext.Update(user);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return new MediatrBaseResult(ResultStatus.Success);
    }
}
