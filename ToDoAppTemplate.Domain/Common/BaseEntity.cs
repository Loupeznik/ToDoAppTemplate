namespace ToDoAppTemplate.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; init; }
    
    public DateTime DateCreated { get; set; }
    
    public DateTime? DateUpdated { get; set; }
    
    public bool IsDeleted { get; set; }
}
