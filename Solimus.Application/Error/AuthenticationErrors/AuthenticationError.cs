using Solimus.Application.Common.Results;

namespace Solimus.Application.Error.AuthenticationErrors;

public static class AuthenticationError
{
    public static Common.Results.Error InvalidRegistrationRequest => new (ErrorTypeConstant.ValidationError, "Неправильно заполнены данные в запросе.");
    public static Common.Results.Error UserAlreadyExists => new(ErrorTypeConstant.ValidationError, "Пользователь с данной эл. почтой уже существует.");
    public static Common.Results.Error InvalidLoginRequest => new(ErrorTypeConstant.ValidationError, "Неправильный логин или пароль.");
    public static Common.Results.Error UserNotFound => new(ErrorTypeConstant.NotFound, "Пользователь не найден.");

    public static Common.Results.Error CreateInvalidLoginRequestError(IEnumerable<string> errors) =>
        new(ErrorTypeConstant.ValidationError, string.Join(" ", errors));
}
