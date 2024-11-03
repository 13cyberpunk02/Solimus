using Solimus.Application.Common.Results;

namespace Solimus.Application.Error.AuthenticationErrors;

public static class AuthenticationError
{
    public static Common.Results.Error InvalidRegistrationRequest => new (ErrorTypeConstant.ValidationError, "Неправильно заполнены данные в запросе.");
    public static Common.Results.Error UserAlreadyExists => new(ErrorTypeConstant.ValidationError, "Пользователь с данной эл. почтой уже существует.");
    public static Common.Results.Error InvalidLoginRequest => new(ErrorTypeConstant.ValidationError, "Неправильный логин или пароль.");
    public static Common.Results.Error UserNotFound => new(ErrorTypeConstant.NotFound, "Пользователь не найден.");
    public static Common.Results.Error EmailEmptyOrNull => new(ErrorTypeConstant.NotFound, "Электронная почта пустая.");
    public static Common.Results.Error EmailNotConfirmed => new(ErrorTypeConstant.NotFound, "Электронная почта данного пользователя не подтверждена.");
    public static Common.Results.Error EmailConfirmed => new(ErrorTypeConstant.ValidationError, "Электронная почта данного пользователя подтверждена.");
    public static Common.Results.Error EmailSendingFailure => new(ErrorTypeConstant.InternalServerError, "Произошла ошибка при отправке письма с подтверждением.");
    public static Common.Results.Error InvalidTokenRequest => new(ErrorTypeConstant.ValidationError, "Токен для подтверждения не прошел валидацию, попробуйте позже.");    
    public static Common.Results.Error TokenUsed => new(ErrorTypeConstant.InternalServerError, "Произошла ошибка при подтверждении, либо ссылка уже была использована.");
    public static Common.Results.Error ErrorRequest => new(ErrorTypeConstant.UnrecognizedRequestError, "Отправлен неправильный запрос с клиента.");

    public static Common.Results.Error CreateInvalidLoginRequestError(IEnumerable<string> errors) =>
        new(ErrorTypeConstant.ValidationError, string.Join(Environment.NewLine, errors));
}
