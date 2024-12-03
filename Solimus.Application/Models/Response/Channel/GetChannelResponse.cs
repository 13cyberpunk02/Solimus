namespace Solimus.Application.Models.Response.Channel;

public record GetChannelResponse(
    string Id,
    string Name,
    string? CategoryId,
    string? LogotypeId,
    string? Source
    );
