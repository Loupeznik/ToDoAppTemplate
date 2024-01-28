namespace ToDoAppTemplate.Domain.Common;

public interface IAuthenticatedRequest
{
    public int? UserId { get; set; }
}
