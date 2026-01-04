namespace Solimus.Application.Common;

public class Result<T> : Result
{
    private Result(T value) : base(true, Error.None)
    {
        Value = value;
    }
    
    private Result(Error error) : base(false, error)
    {
        Value = default;
    }
    
    public T Value => IsSuccess ? 
        field! : 
        throw new InvalidOperationException("Нет доступа к значению результата, когда результат с ошибкой");

    public static Result<T> Success(T value) => new(value);
    public new static Result<T> Failure(Error error) => new(error);
    
    public static implicit operator Result<T>(Error error) => Failure(error);
    public static implicit operator Result<T>(T value) => Success(value);
}