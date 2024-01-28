using FastEndpoints;
using FluentValidation;
using ToDoAppTemplate.Api.Auth.Models;

namespace ToDoAppTemplate.Api.Auth.Validators;

public class RequestPasswordResetRequestValidator : Validator<RequestPasswordResetRequest>
{
    public RequestPasswordResetRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login cannot be empty.");
    }
}
