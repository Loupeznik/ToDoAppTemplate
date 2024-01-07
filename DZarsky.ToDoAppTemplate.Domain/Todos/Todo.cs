using DZarsky.ToDoAppTemplate.Domain.Common;
using DZarsky.ToDoAppTemplate.Domain.Users;

namespace DZarsky.ToDoAppTemplate.Domain.Todos;

public sealed class Todo : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public bool IsComplete { get; set; }
    
    public DateTime? DateCompleted { get; set; }
    
    public int OwnerId { get; set; }
    
    public User? Owner { get; set; }
    
    public IList<string> Tags { get; set; } = new List<string>();
}
