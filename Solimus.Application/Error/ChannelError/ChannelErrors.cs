using Solimus.Application.Common.Results;

namespace Solimus.Application.Error.ChannelError;

public static class ChannelErrors 
{
    public static Common.Results.Error ChannelNotFound => new(ErrorTypeConstant.NotFound, "Канал не найден");
    public static Common.Results.Error ChannelExists => new(ErrorTypeConstant.ValidationError, "Канал уже существует");
    public static Common.Results.Error ChannelDoesntExist => new(ErrorTypeConstant.ValidationError, "Такого канала не существует");
    public static Common.Results.Error ChannelAddError => new(ErrorTypeConstant.UnrecognizedRequestError, "Ошибка при сохранении канала в БД");
    public static Common.Results.Error ListOfErrors(IEnumerable<string> errors) =>
        new(ErrorTypeConstant.ValidationError, string.Join(Environment.NewLine, errors));
}