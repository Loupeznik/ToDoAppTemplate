namespace ToDoAppTemplate.Domain.Common.Results;

public enum ResultStatus
{
    Success,
    ValidationError,
    EntityNotFound,
    EntityCreated,
    AlreadyExists,
    InternalError,
    Unauthorized
}
