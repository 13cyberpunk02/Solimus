namespace Solimus.Application.Common.ServiceErrors;

public static class AuthenticationErrors
{
    public static Error InvalidCredentials =>
        new(ErrorTypes.BadRequest, "Пароль или эл. почта указана неверно.");
    
    public static Error UserNotFound =>
        new(ErrorTypes.NotFound, "Пользователь не найден.");
    
    public static Error UserAlreadyLoggedOut =>
        new(ErrorTypes.BadRequest, "Пользователь не авторизован.");
    
    public static Error InvalidCredentialsWithIncorrectPassword(int count) =>
        new(ErrorTypes.BadRequest, $"Пароль или эл. почта указана неверно. У вас осталось {count} попыток для входа.");
    
    public static Error AlreadyRegistered =>
        new(ErrorTypes.BadRequest, "Вы уже зарегистрированы");
    
    public static Error EmailNotFound =>
        new(ErrorTypes.NotFound, "Эл. почта не найдена");
    
    public static Error InvalidToken => new(
        ErrorTypes.BadRequest, 
        "Недействительный токен доступа");
    
    public static Error UserLockoutBan(DateTime lockoutTime) =>
        new(ErrorTypes.BadRequest, 
            $"Вы были заблокированы до {lockoutTime.ToShortTimeString()}, из-за множественных попыток входа неверными данными.");
}