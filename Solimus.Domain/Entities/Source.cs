namespace Solimus.Domain.Entities;

public class Source
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Url { get; set; } = string.Empty;
    public Guid? ChannelId { get; set; }
    public Channel? Channel { get; set; }
}
