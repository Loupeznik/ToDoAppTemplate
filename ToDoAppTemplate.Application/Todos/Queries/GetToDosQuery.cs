using ToDoAppTemplate.Data.Infrastructure.EF;
using ToDoAppTemplate.Domain.Common;
using ToDoAppTemplate.Domain.Common.MediatR;
using ToDoAppTemplate.Domain.Common.Requests;
using ToDoAppTemplate.Domain.Common.Results;
using ToDoAppTemplate.Domain.Todos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ToDoAppTemplate.Application.Todos.Queries;

public sealed class GetToDosQuery : PagedRequest, IRequest<MediatrBaseResult<PagedResponse<IList<Todo>>>>,
    IAuthenticatedRequest
{
    public bool GetDeleted { get; set; }

    public bool GetCompleted { get; set; }

    public int? UserId { get; set; }

    public GetToDosQuery(bool? getDeleted = null, bool? getCompleted = null, int? page = null, int? pageSize = null)
    {
        GetDeleted = getDeleted.GetValueOrDefault();
        GetCompleted = getCompleted.GetValueOrDefault();
        Page = page ?? 1;
        PageSize = pageSize ?? 10;
    }
}

public sealed class GetToDosQueryHandler : IRequestHandler<GetToDosQuery, MediatrBaseResult<PagedResponse<IList<Todo>>>>
{
    private readonly DataContext _dataContext;

    public GetToDosQueryHandler(DataContext dataContext) => _dataContext = dataContext;

    public async Task<MediatrBaseResult<PagedResponse<IList<Todo>>>> Handle(GetToDosQuery request,
        CancellationToken cancellationToken)
    {
        var query = _dataContext.Set<Todo>()
                                .Where(x => x.OwnerId == request.UserId)
                                .AsNoTracking();

        if (request.GetCompleted)
        {
            query = query.Where(x => x.IsComplete);
        }

        if (request.GetDeleted)
        {
            query = query.IgnoreQueryFilters();
        }

        var totalCount = query.Count();
        var totalPageCount = totalCount / request.PageSize;

        query = query.OrderByDescending(x => x.DateCreated);

        var todos = await query
                          .Skip(request.Page > 1 ? request.PageSize * (request.Page - 1) : 0)
                          .Take(request.PageSize)
                          .ToListAsync(cancellationToken);

        return new MediatrBaseResult<PagedResponse<IList<Todo>>>(ResultStatus.Success,
            new PagedResponse<IList<Todo>>(request.Page, request.PageSize, totalCount, totalPageCount, todos));
    }
}
