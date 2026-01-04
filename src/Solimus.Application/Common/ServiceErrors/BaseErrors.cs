namespace Solimus.Application.Common.ServiceErrors;

public class BaseErrors
{
    public static Error DatabaseError => 
        new Error("Ошибка чтения БД", "Возникла ошибка на стороне базы данных");

    public static Error ValidationError(string message) => 
        new Error(ErrorTypes.Validation, message);

    public static Error UnexpectedError => 
        new Error(ErrorTypes.InternalServerError, "Возникла неизвестная ошибка");
}