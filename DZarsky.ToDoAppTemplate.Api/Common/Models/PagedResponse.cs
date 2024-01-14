namespace DZarsky.ToDoAppTemplate.Api.Common.Models;

public class PagedResponse<TResponse> where TResponse : class
{
    public int Page { get; set; }
    
    public int PageSize { get; set; }
    
    public int TotalCount { get; set; }
    
    public int TotalPages { get; set; }
    
    public IEnumerable<TResponse> Data { get; set; } = new List<TResponse>();
}
