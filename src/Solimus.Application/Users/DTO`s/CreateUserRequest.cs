namespace Solimus.Application.Users.DTO_s;

public record CreateUserRequest(
    string Email,
    string UserName,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName);