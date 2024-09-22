﻿namespace Solimus.Application.Common.Results;

public static class ErrorTypeConstant
{
    public const string None = "Пусто.";
    public const string ValidationError = "Ошибка валидации.";
    public const string UnAuthorized = "Ошибка авторизации.";
    public const string NotFound = "Ошибка, ничего не найдено.";
    public const string InternalServerError = "Ошибка сервера.";
    public const string Forbidden = "Ошибка доступа";
}
