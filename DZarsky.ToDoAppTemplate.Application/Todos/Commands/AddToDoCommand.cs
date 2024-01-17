using DZarsky.ToDoAppTemplate.Data.Infrastructure.EF;
using DZarsky.ToDoAppTemplate.Domain.Common;
using DZarsky.ToDoAppTemplate.Domain.Common.MediatR;
using DZarsky.ToDoAppTemplate.Domain.Common.Results;
using DZarsky.ToDoAppTemplate.Domain.Todos;
using MediatR;

namespace DZarsky.ToDoAppTemplate.Application.Todos.Commands;

public sealed class AddToDoCommand : IRequest<MediatrBaseResult<Todo>>, IAuthenticatedRequest
{
    public int? UserId { get; set; }
    
    public string Title { get; set; }
    
    public string? Description { get; set; }
    
    public AddToDoCommand(string title, string? description = null)
    {
        Title = title;
        Description = description;
    }
}

public sealed class AddToDoCommandHandler : IRequestHandler<AddToDoCommand, MediatrBaseResult<Todo>>
{
    private readonly DataContext _dataContext;

    public AddToDoCommandHandler(DataContext dataContext) => _dataContext = dataContext;

    public async Task<MediatrBaseResult<Todo>> Handle(AddToDoCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
        {
            return new MediatrBaseResult<Todo>(ResultStatus.Unauthorized, new Todo());
        }
        
        var todo = new Todo
        {
            Title = request.Title,
            Description = request.Description,
            OwnerId = request.UserId.GetValueOrDefault()
        };

        await _dataContext.Set<Todo>().AddAsync(todo, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return new MediatrBaseResult<Todo>(ResultStatus.EntityCreated, todo);
    }
}
