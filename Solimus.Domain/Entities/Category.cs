namespace Solimus.Domain.Entities;

public class Category
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<Channel>? Channels { get; set; }
}
