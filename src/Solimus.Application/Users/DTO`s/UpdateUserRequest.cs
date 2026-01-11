namespace Solimus.Application.Users.DTO_s;

public record UpdateUserRequest(
    string Email,
    string UserName,
    string FirstName,
    string LastName);