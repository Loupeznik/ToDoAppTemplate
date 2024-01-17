using DZarsky.ToDoAppTemplate.Data.Infrastructure.EF;
using DZarsky.ToDoAppTemplate.Domain.Common;
using DZarsky.ToDoAppTemplate.Domain.Common.MediatR;
using DZarsky.ToDoAppTemplate.Domain.Common.Results;
using DZarsky.ToDoAppTemplate.Domain.Todos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DZarsky.ToDoAppTemplate.Application.Todos.Queries;

public sealed class GetToDosQuery : IRequest<MediatrBaseResult<IList<Todo>>>, IAuthenticatedRequest
{
    public bool GetDeleted { get; set; }
    
    public int? UserId { get; set; }
}

public sealed class GetToDosQueryHandler : IRequestHandler<GetToDosQuery, MediatrBaseResult<IList<Todo>>>
{
    private readonly DataContext _dataContext;

    public GetToDosQueryHandler(DataContext dataContext) => _dataContext = dataContext;

    public async Task<MediatrBaseResult<IList<Todo>>> Handle(GetToDosQuery request, CancellationToken cancellationToken)
    {
        // TODO: Add filtering
        var todos = await _dataContext.Set<Todo>().Where(x => x.OwnerId == request.UserId).ToListAsync(cancellationToken: cancellationToken);

        return new MediatrBaseResult<IList<Todo>>(ResultStatus.Success, todos);
    }
}
