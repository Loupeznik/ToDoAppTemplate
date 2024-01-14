using DZarsky.ToDoAppTemplate.Domain.Common.MediatR;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Application.Todos.Commands;

public sealed class DeleteToDoCommand : IRequest<MediatrBaseResult>
{
    public int? UserId { get; set; }
    
    public int Id { get; set; }
    
    public DeleteToDoCommand(int id, int? userId)
    {
        Id = id;
        UserId = userId;
    }
}
