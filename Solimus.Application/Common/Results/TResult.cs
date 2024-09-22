namespace Solimus.Application.Common.Results;

public class TResult<TValue> : Result
{
    private readonly TValue _value;

    protected internal TResult(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("Нет такой ошибки для назначения");
}
