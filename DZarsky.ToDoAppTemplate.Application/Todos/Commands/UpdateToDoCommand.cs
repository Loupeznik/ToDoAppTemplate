using DZarsky.ToDoAppTemplate.Domain.Common.MediatR;
using DZarsky.ToDoAppTemplate.Domain.Todos;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Application.Todos.Commands;

public sealed class UpdateToDoCommand : IRequest<MediatrBaseResult<Todo>>
{
    public int? UserId { get; set; }
    
    public int Id { get; set; }
    
    public string Title { get; init; }
    
    public string? Description { get; init; }
    
    public bool? IsComplete { get; init; }
    
    public DateTime? DateCompleted { get; init; }
    
    public UpdateToDoCommand(int id, string title, int? userId, string? description, bool? isComplete, DateTime? dateCompleted)
    {
        Id = id;
        UserId = userId;
        Title = title;
        Description = description;
        IsComplete = isComplete;
        DateCompleted = dateCompleted;
    }
}
