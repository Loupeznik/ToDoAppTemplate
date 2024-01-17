using DZarsky.ToDoAppTemplate.Data.Infrastructure.EF;
using DZarsky.ToDoAppTemplate.Domain.Common;
using DZarsky.ToDoAppTemplate.Domain.Common.MediatR;
using DZarsky.ToDoAppTemplate.Domain.Common.Results;
using DZarsky.ToDoAppTemplate.Domain.Todos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DZarsky.ToDoAppTemplate.Application.Todos.Commands;

public sealed class DeleteToDoCommand : IRequest<MediatrBaseResult>, IAuthenticatedRequest
{
    public int? UserId { get; set; }

    public int Id { get; set; }

    public DeleteToDoCommand(int id) => Id = id;
}

public sealed class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand, MediatrBaseResult>
{
    private readonly DataContext _dataContext;

    public DeleteToDoCommandHandler(DataContext dataContext) => _dataContext = dataContext;

    public async Task<MediatrBaseResult> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _dataContext.Set<Todo>()
                                     .FirstOrDefaultAsync(x => x.Id == request.Id && x.OwnerId == request.UserId,
                                         cancellationToken: cancellationToken);

        if (todo is null)
        {
            return new MediatrBaseResult(ResultStatus.EntityNotFound);
        }

        todo.IsDeleted = true;
        
        _dataContext.Update(todo);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return new MediatrBaseResult(ResultStatus.Success);
    }
}
