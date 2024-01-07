namespace DZarsky.ToDoAppTemplate.Domain.Common;

public class BaseEntity
{
    public int Id { get; init; }
    
    public DateTime DateCreated { get; set; }
    
    public DateTime? DateUpdated { get; set; }
    
    public bool IsDeleted { get; set; }
}
