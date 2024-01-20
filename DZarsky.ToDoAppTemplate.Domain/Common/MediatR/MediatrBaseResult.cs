using System.Runtime.CompilerServices;
using DZarsky.ToDoAppTemplate.Domain.Common.Errors;
using DZarsky.ToDoAppTemplate.Domain.Common.Results;

namespace DZarsky.ToDoAppTemplate.Domain.Common.MediatR;

public class MediatrBaseResult<TResult> : IMediatrBaseResult where TResult : class
{
    public bool IsSuccess => Status is ResultStatus.Success or ResultStatus.EntityCreated;

    public string? Message { get; set; }

    public ResultStatus Status { get; set; }

    public TResult? Result { get; set; }
    
    public IList<ErrorDescription> Errors { get; set; } = new List<ErrorDescription>();

    public MediatrBaseResult(ResultStatus status, TResult? result = null, IList<ErrorDescription>? errors = null, string? message = null)
    {
        Message = message;
        Result = result;
        Status = status;

        if (errors != null)
        {
            Errors = errors;
        }
    }

    public MediatrBaseResult(ResultStatus status, IList<ErrorDescription>? errors = null, string? message = null)
    {
        Message = message;
        Status = status;
        
        if (errors != null)
        {
            Errors = errors;
        }
    }
}

public class MediatrBaseResult : IMediatrBaseResult
{
    public bool IsSuccess => Status == ResultStatus.Success;

    public string? Message { get; set; }

    public ResultStatus Status { get; set; }

    public IList<ErrorDescription> Errors { get; set; } = new List<ErrorDescription>();

    public MediatrBaseResult(ResultStatus status, IList<ErrorDescription>? errors = null, string? message = null)
    {
        Message = message;
        Status = status;
        
        if (errors != null)
        {
            Errors = errors;
        }
    }
}
