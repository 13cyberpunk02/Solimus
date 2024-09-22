namespace Solimus.Application.Common.Results;

public class Result
{
    protected bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    protected Result(bool success, Error error)
    {
        if ((success && error != Error.None) || (!success && error == Error.None))
        {
            throw new InvalidOperationException("Невозможно выполнить операцию");
        }

        IsSuccess = success;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static TResult<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}
