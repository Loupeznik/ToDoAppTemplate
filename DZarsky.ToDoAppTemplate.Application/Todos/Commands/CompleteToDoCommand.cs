using DZarsky.ToDoAppTemplate.Domain.Common.MediatR;
using DZarsky.ToDoAppTemplate.Domain.Todos;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Application.Todos.Commands;

public sealed class CompleteToDoCommand : IRequest<MediatrBaseResult<Todo>>
{
    public int? UserId { get; set; }
    
    public int Id { get; set; }
    
    public DateTime? CompletedDate { get; set; }
    
    public CompleteToDoCommand(int id, DateTime? completedDate, int? userId)
    {
        Id = id;
        CompletedDate = completedDate;
        UserId = userId;
    }
}
