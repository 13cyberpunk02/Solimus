namespace Solimus.Application.Authentication.DTO_s;

public record RegistrationRequest(
    string Email, 
    string UserName, 
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName);