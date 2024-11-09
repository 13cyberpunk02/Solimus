namespace Solimus.Application.Models.Request.Authentication;

public record RegistrationRequest(string Email, string Password, string ConfirmPassword, string Username);
