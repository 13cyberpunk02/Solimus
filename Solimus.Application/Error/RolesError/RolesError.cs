using Solimus.Application.Common.Results;

namespace Solimus.Application.Error.RolesError;

public static class RolesError
{
    public static Common.Results.Error RoleNotFound => new(ErrorTypeConstant.NotFound, "Роль не найден");
    public static Common.Results.Error RoleExists => new(ErrorTypeConstant.ValidationError, "Роль уже существует");
    public static Common.Results.Error RoleDoesntExist => new(ErrorTypeConstant.ValidationError, "Такой роли не существует");    
    public static Common.Results.Error RoleNameEmpty => new(ErrorTypeConstant.ValidationError, "Нет имени роли");
    public static Common.Results.Error RoleIdEmpty => new(ErrorTypeConstant.ValidationError, "Нет Id роли");

    public static Common.Results.Error ListOfErrors(IEnumerable<string> errors) =>
        new(ErrorTypeConstant.ValidationError, string.Join(Environment.NewLine, errors));
}
