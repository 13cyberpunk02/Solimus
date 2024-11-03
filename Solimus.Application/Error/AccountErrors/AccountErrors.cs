using Solimus.Application.Common.Results;

namespace Solimus.Application.Error.AccountErrors;

public static class AccountErrors
{
    public static Common.Results.Error UserNotFound => new(ErrorTypeConstant.NotFound, "Пользователь не найден.");


    public static Common.Results.Error ListOfErrors(IEnumerable<string> errors) =>
        new(ErrorTypeConstant.ValidationError, string.Join(Environment.NewLine, errors));
}
