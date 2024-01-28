using FastEndpoints;
using FluentValidation;
using ToDoAppTemplate.Api.Auth.Models;

namespace ToDoAppTemplate.Api.Auth.Validators;

public class RegisterRequestValidator : Validator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login cannot be empty.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");
    }
}
