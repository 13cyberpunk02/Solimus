namespace Solimus.Application.Common.ServiceErrors;

public static class UserErrors
{
    public static Error NotFound =>
        new(ErrorTypes.NotFound, "Пользователь не найден");
    
    public static Error EmailIsReserved(string email) =>
        new(ErrorTypes.BadRequest, $"Эл. почта {email} занята уже другим пользователем, либо вами.");
}