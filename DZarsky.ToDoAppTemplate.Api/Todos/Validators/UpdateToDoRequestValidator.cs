using DZarsky.ToDoAppTemplate.Api.Todos.Models.Requests;
using FastEndpoints;
using FluentValidation;

namespace DZarsky.ToDoAppTemplate.Api.Todos.Validators;

public sealed class UpdateToDoRequestValidator : Validator<UpdateToDoRequest>
{
    public UpdateToDoRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("No Id provided.");
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty.")
            .MaximumLength(255)
            .WithMessage("Title cannot be longer than 255 characters.");
        
        RuleFor(x => x.Description)
            .MaximumLength(2048)
            .WithMessage("Description cannot be longer than 2048 characters.");

        RuleFor(x => x.DateCompleted)
            .LessThanOrEqualTo(DateTime.Now);
    }
}
