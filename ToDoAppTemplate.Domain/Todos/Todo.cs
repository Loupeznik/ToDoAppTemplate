using ToDoAppTemplate.Domain.Common;
using ToDoAppTemplate.Domain.Users;

namespace ToDoAppTemplate.Domain.Todos;

public sealed class Todo : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public bool IsComplete { get; set; }
    
    public DateTime? DateCompleted { get; set; }
    
    public int OwnerId { get; set; }
    
    public User? Owner { get; set; }
}
