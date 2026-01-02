namespace Solimus.Domain.Entities;

public class Role
{
    public Guid RoleId { get; set; } = Guid.CreateVersion7();
    public string RoleName { get; set; } = string.Empty;

    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
}