using DZarsky.ToDoAppTemplate.Domain.Common.MediatR;
using DZarsky.ToDoAppTemplate.Domain.Todos;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Application.Todos.Queries;

public sealed class GetToDosQuery : IRequest<MediatrBaseResult<IList<Todo>>>
{
    
}
