namespace Solimus.Application.Models.Request.Channel;

public record CreateChannelRequest(
    string Name, 
    Guid? CategoryId, 
    Guid? LogotypeId, 
    Guid? SourceId);
