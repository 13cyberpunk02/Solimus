namespace Solimus.Domain.Entities;

public class Logotype
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Uri { get; set; } = string.Empty;
    public List<Channel>? Channels { get; set; }
}
