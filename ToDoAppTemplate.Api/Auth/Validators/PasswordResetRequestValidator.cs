using FastEndpoints;
using FluentValidation;
using ToDoAppTemplate.Api.Auth.Models;

namespace ToDoAppTemplate.Api.Auth.Validators;

public sealed class PasswordResetRequestValidator : Validator<PasswordResetRequest>
{
    public PasswordResetRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login cannot be empty.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Code cannot be empty.")
            .Length(8)
            .WithMessage("Code must be 8 characters long.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("New password cannot be empty.")
            .MinimumLength(8)
            .WithMessage("New password must be at least 8 characters long.");
    }
}
