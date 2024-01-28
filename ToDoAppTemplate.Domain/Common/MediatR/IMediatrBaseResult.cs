using ToDoAppTemplate.Domain.Common.Errors;
using ToDoAppTemplate.Domain.Common.Results;

namespace ToDoAppTemplate.Domain.Common.MediatR;

public interface IMediatrBaseResult
{
    bool IsSuccess { get; }

    string? Message { get; set; }
    
    ResultStatus Status { get; set; }
    
    IList<ErrorDescription> Errors { get; set; }
}
