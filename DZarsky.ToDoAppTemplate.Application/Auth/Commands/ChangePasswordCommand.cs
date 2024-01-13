using DZarsky.ToDoAppTemplate.Domain.Common.MediatR;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Application.Auth.Commands;

public sealed class ChangePasswordCommand : IRequest<MediatrBaseResult>
{
    public int UserId { get; set; }
    
    public string OldPassword { get; set; }
    
    public string NewPassword { get; set; }
    
    public ChangePasswordCommand(int userId, string oldPassword, string newPassword)
    {
        UserId = userId;
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }
}

public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, MediatrBaseResult>
{

    public async Task<MediatrBaseResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
