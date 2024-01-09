namespace DZarsky.ToDoAppTemplate.Domain.Common.MediatR;

public class MediatrBaseResult<TResult, TResultStatus> : IMediatrBaseResult where TResult : class 
{
    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public TResultStatus Status { get; set; }

    public TResult? Result { get; set; }

    public MediatrBaseResult(bool isSuccess, TResultStatus status, TResult? result = null, string? message = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Result = result;
        Status = status;
    }

    public MediatrBaseResult(TResultStatus status, string? message = null)
    {
        Message = message;
        Status = status;
    }
}

public class MediatrBaseResult<TResultStatus> : IMediatrBaseResult
{
    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public TResultStatus? Status { get; set; }
    
    public IList<string> Errors { get; set; } = new List<string>();

    public MediatrBaseResult(bool isSuccess, TResultStatus status, IList<string>? errors, string? message = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Status = status;
        Errors = errors ?? new List<string>();
    }

    public MediatrBaseResult()
    {
    }
}
