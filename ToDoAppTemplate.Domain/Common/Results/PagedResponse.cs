namespace ToDoAppTemplate.Domain.Common.Results;

public class PagedResponse<TResponse> where TResponse : class
{
    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public int TotalPages { get; set; }

    public TResponse Data { get; set; }

    public PagedResponse(int page, int pageSize, int totalCount, int totalPages, TResponse data)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = totalPages;
        Data = data;
    }
}
