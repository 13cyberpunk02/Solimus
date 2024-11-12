namespace Solimus.Application.Models.Request.User;

public record UserUpdateRequest(
    string Id,
    string Firstname,
    string Lastname,
    DateTime Birthday,
    string PhoneNumber,
    string Address);
