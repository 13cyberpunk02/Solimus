namespace Solimus.Application.Models.Request.Authentication;

public record ResetPasswordRequest(string Token, string Email, string NewPassword);
