namespace Solimus.Application.Common.ServiceErrors;

public static class UserErrors
{
    public static Error NotFound =>
        new(ErrorTypes.NotFound, "Пользователь не найден");
}