using ToDoAppTemplate.Core.Infrastructure.Communication.Email;
using ToDoAppTemplate.Core.Infrastructure.Security;
using ToDoAppTemplate.Core.Templating;
using ToDoAppTemplate.Data.Infrastructure.EF;
using ToDoAppTemplate.Domain.Common.Errors;
using ToDoAppTemplate.Domain.Common.MediatR;
using ToDoAppTemplate.Domain.Common.Results;
using ToDoAppTemplate.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ToDoAppTemplate.Application.Auth.Commands;

public sealed class RequestPasswordResetCommand : IRequest<MediatrBaseResult>
{
    public string Login { get; init; }

    public RequestPasswordResetCommand(string login) => Login = login;
}

public sealed class RequestPasswordResetCommandHandler : IRequestHandler<RequestPasswordResetCommand, MediatrBaseResult>
{
    private readonly DataContext _dataContext;
    private readonly PasswordHasher _passwordHasher;
    private readonly IEmailSender _emailSender;
    private readonly TemplatingService _templatingService;

    public RequestPasswordResetCommandHandler(DataContext dataContext, PasswordHasher hasher, IEmailSender emailSender,
        TemplatingService templatingService)
    {
        _dataContext = dataContext;
        _passwordHasher = hasher;
        _emailSender = emailSender;
        _templatingService = templatingService;
    }

    public async Task<MediatrBaseResult> Handle(RequestPasswordResetCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _dataContext
                         .Set<User>()
                         .AsNoTracking()
                         .FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken);

        if (user == null)
        {
            return new MediatrBaseResult(ResultStatus.EntityNotFound);
        }

        if (user.Email == null)
        {
            return new MediatrBaseResult(ResultStatus.ValidationError, new List<ErrorDescription>()
            {
                new("Email.NotFound",
                    "Cannot request password reset - no email address is associated with this account.")
            });
        }

        var code = new PasswordResetCode
        {
            Code = PasswordResetCode.GenerateResetCode(),
            UserId = user.Id,
            ExpirationDate = DateTime.UtcNow.AddHours(1)
        };

        await _dataContext.AddAsync(code, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);

        var emailBody = await _templatingService.Render("password-reset", new
        {
            code.Code
        });

        await _emailSender.Send(user.Email, "Password reset request", emailBody);

        return new MediatrBaseResult(ResultStatus.Success);
    }
}
