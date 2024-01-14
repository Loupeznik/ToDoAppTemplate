using DZarsky.ToDoAppTemplate.Domain.Common.MediatR;
using DZarsky.ToDoAppTemplate.Domain.Todos;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Application.Todos.Commands;

public sealed class AddToDoCommand : IRequest<MediatrBaseResult<Todo>>
{
    public int? UserId { get; set; }
    
    public string? Title { get; set; }
    
    public string? Description { get; set; }
    
    public AddToDoCommand(int? userId, string? title, string? description)
    {
        UserId = userId;
        Title = title;
        Description = description;
    }
}
