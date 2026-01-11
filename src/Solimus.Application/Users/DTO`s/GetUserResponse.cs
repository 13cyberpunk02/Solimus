namespace Solimus.Application.Users.DTO_s;

public record GetUserResponse(
    Guid UserId,
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    DateTime JoinedDate,
    DateTime LastUpdate);
    