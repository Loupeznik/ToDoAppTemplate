namespace DZarsky.ToDoAppTemplate.Domain.Common.MediatR;

public class MediatrBaseResult<TClass, TResultStatus> : IMediatrBaseResult where TClass : class
{
    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public TResultStatus Status { get; set; }

    public TClass? Result { get; set; }

    public MediatrBaseResult(bool isSuccess, TResultStatus status, TClass? result = null, string? message = null)
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

    public MediatrBaseResult(bool isSuccess, TResultStatus status, string? message = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Status = status;
    }

    public MediatrBaseResult()
    {
    }
}
