using Microsoft.AspNetCore.Identity;

namespace Solimus.Domain.Entities;

public class SolimusUser : IdentityUser
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Fullname => Firstname + " " + Lastname;
    public string Address { get; set; } = string.Empty;
    public DateTime JoinedDate { get; set; }
    public DateTime Birthday { get; set; }
    public string AvatarImageUrl { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
