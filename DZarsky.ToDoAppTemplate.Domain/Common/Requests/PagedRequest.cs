namespace DZarsky.ToDoAppTemplate.Domain.Common.Requests;

public class PagedRequest
{
    public int Page { get; protected init; } = 1;

    public int PageSize { get; protected init; } = 10;
}
