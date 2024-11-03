using Microsoft.AspNetCore.Identity;

namespace Solimus.Application.DTO_s.User;

public class UserResponseDto
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public DateTime JoinedDate { get; set; }
    public List<string>? Roles { get; set; }
}
