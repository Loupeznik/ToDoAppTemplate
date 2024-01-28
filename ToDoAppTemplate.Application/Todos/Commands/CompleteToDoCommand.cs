using ToDoAppTemplate.Data.Infrastructure.EF;
using ToDoAppTemplate.Domain.Common;
using ToDoAppTemplate.Domain.Common.MediatR;
using ToDoAppTemplate.Domain.Common.Results;
using ToDoAppTemplate.Domain.Todos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ToDoAppTemplate.Application.Todos.Commands;

public sealed class CompleteToDoCommand : IRequest<MediatrBaseResult<Todo>>, IAuthenticatedRequest
{
    public int? UserId { get; set; }
    
    public int Id { get; set; }
    
    public DateTime? DateCompleted { get; set; }
    
    public CompleteToDoCommand(int id, DateTime? dateCompleted = null)
    {
        Id = id;
        DateCompleted = dateCompleted;
    }
}

public sealed class CompleteToDoCommandHandler : IRequestHandler<CompleteToDoCommand, MediatrBaseResult<Todo>>
{
    private readonly DataContext _dataContext;

    public CompleteToDoCommandHandler(DataContext dataContext) => _dataContext = dataContext;

    public async Task<MediatrBaseResult<Todo>> Handle(CompleteToDoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _dataContext.Set<Todo>()
                                     .FirstOrDefaultAsync(x => x.Id == request.Id && x.OwnerId == request.UserId,
                                         cancellationToken: cancellationToken);

        if (todo is null)
        {
            return new MediatrBaseResult<Todo>(ResultStatus.EntityNotFound, new Todo());
        }

        todo.IsComplete = true;
        todo.DateCompleted = request.DateCompleted ?? DateTime.UtcNow;

        _dataContext.Update(todo);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return new MediatrBaseResult<Todo>(ResultStatus.Success, todo);
    }
}
