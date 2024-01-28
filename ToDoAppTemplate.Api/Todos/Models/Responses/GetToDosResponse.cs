using ToDoAppTemplate.Domain.Common.Results;
using ToDoAppTemplate.Domain.Todos;

namespace ToDoAppTemplate.Api.Todos.Models.Responses;

public sealed class GetToDosResponse : PagedResponse<IList<GetToDoResponse>>
{
    private GetToDosResponse(int page, int pageSize, int totalCount, int totalPages, IList<GetToDoResponse> data) :
        base(page,
            pageSize, totalCount, totalPages, data)
    {
    }

    internal static GetToDosResponse MapFromQueryResult(PagedResponse<IList<Todo>> result)
    {
        return new GetToDosResponse(result.Page, result.PageSize, result.TotalCount, result.TotalPages,
            result.Data.Select(GetToDoResponse.MapFromToDo).ToList());
    }

    internal static GetToDosResponse GetEmptyResponse()
    {
        return new GetToDosResponse(0, 0, 0, 0, new List<GetToDoResponse>());
    }
}
