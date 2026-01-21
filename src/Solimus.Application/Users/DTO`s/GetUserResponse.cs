namespace Solimus.Application.Users.DTO_s;

public record GetUserResponse(
    Guid UserId,
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    List<string> RoleNames,
    DateTime JoinedDate,
    DateTime LastUpdate);
    