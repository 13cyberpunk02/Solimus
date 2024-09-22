namespace Solimus.Domain.Entities;

public class Channel
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    public Guid? LogotypeId { get; set; }
    public Logotype? Logotype { get; set; }
    public Source? Source { get; set; }
}
