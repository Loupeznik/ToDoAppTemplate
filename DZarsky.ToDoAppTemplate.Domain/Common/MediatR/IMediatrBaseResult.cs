namespace DZarsky.ToDoAppTemplate.Domain.Common.MediatR;

public interface IMediatrBaseResult
{
    bool IsSuccess { get; set; }

    string? Message { get; set; }
}
