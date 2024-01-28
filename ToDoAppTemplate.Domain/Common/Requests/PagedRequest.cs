// ReSharper disable MemberCanBeProtected.Global
namespace ToDoAppTemplate.Domain.Common.Requests;

public class PagedRequest
{
    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
