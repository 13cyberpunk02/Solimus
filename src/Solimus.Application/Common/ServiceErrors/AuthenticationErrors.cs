namespace Solimus.Application.Common.ServiceErrors;

public static class AuthenticationErrors
{
    public static Error InvalidCredentials =>
        new(ErrorTypes.BadRequest, "Пароль или эл. почта указана неверно");
    
    public static Error AlreadyRegistered =>
        new(ErrorTypes.BadRequest, "Вы уже зарегистрированы");
    
    public static Error EmailNotFound =>
        new(ErrorTypes.NotFound, "Эл. почта не найдена");
}