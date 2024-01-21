using DZarsky.ToDoAppTemplate.Api.Auth.Models;
using FastEndpoints;
using FluentValidation;

namespace DZarsky.ToDoAppTemplate.Api.Auth.Validators;

public class RequestPasswordResetRequestValidator : Validator<RequestPasswordResetRequest>
{
    public RequestPasswordResetRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login cannot be empty.");
    }
}
