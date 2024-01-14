namespace DZarsky.ToDoAppTemplate.Api.Todos.Models.Responses;

public class GetToDoResponse
{
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public bool IsComplete { get; set; }
    
    public DateTime? DateCompleted { get; set; }
}
