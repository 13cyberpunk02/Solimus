namespace Solimus.Domain.Entities;

public class User
{
    public Guid UserId { get; set; } = Guid.CreateVersion7();

    public string Email { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public string UserName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdateTime { get; set; } = DateTime.UtcNow;
    public DateTime? LockOutTime { get; set; }

    public int IncorrectPasswordCount { get; set; } = 0;
    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    public virtual RefreshToken? RefreshToken { get; set; }
}