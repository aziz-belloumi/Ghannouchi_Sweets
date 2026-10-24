namespace Sweets.Domain.Common;

public class Result
{
    public bool IsSuccess { get; }
    public string? ErrorCode { get; }
    public string? ErrorMessage { get; }

    protected Result(bool isSuccess, string? errorCode, string? errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public static Result Success() => new Result(true, null, null);

    public static Result Failure(string errorCode, string errorMessage) => new Result(false, errorCode, errorMessage);
}