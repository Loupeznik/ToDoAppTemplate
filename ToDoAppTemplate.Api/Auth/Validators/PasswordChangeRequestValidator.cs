using FastEndpoints;
using FluentValidation;
using ToDoAppTemplate.Api.Auth.Models;

namespace ToDoAppTemplate.Api.Auth.Validators;

public sealed class PasswordChangeRequestValidator : Validator<PasswordChangeRequest>
{
    public PasswordChangeRequestValidator()
    {
        RuleFor(x => x.OldPassword)
            .Equal(x => x.NewPassword)
            .WithMessage("New password cannot be the same as old password.");

        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.");
    }
}
