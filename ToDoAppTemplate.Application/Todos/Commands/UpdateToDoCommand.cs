using ToDoAppTemplate.Data.Infrastructure.EF;
using ToDoAppTemplate.Domain.Common;
using ToDoAppTemplate.Domain.Common.MediatR;
using ToDoAppTemplate.Domain.Common.Results;
using ToDoAppTemplate.Domain.Todos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ToDoAppTemplate.Application.Todos.Commands;

public sealed class UpdateToDoCommand : IRequest<MediatrBaseResult<Todo>>, IAuthenticatedRequest
{
    public int? UserId { get; set; }
    
    public int Id { get; set; }
    
    public string Title { get; init; }
    
    public string? Description { get; init; }
    
    public bool? IsComplete { get; init; }
    
    public DateTime? DateCompleted { get; init; }
    
    public UpdateToDoCommand(int id, string title, string? description, bool? isComplete, DateTime? dateCompleted)
    {
        Id = id;
        Title = title;
        Description = description;
        IsComplete = isComplete;
        DateCompleted = dateCompleted;
    }
}

public sealed class UpdateToDoCommandHandler : IRequestHandler<UpdateToDoCommand, MediatrBaseResult<Todo>>
{
    private readonly DataContext _dataContext;

    public UpdateToDoCommandHandler(DataContext dataContext) => _dataContext = dataContext;

    public async Task<MediatrBaseResult<Todo>> Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _dataContext.Set<Todo>()
                                     .FirstOrDefaultAsync(x => x.Id == request.Id && x.OwnerId == request.UserId,
                                         cancellationToken: cancellationToken);

        if (todo is null)
        {
            return new MediatrBaseResult<Todo>(ResultStatus.EntityNotFound, new Todo());
        }

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.IsComplete = request.IsComplete ?? todo.IsComplete;
        todo.DateCompleted = request.DateCompleted ?? todo.DateCompleted;

        _dataContext.Update(todo);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return new MediatrBaseResult<Todo>(ResultStatus.Success, todo);
    }
}
