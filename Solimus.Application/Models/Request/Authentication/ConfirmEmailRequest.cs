namespace Solimus.Application.Models.Request.Authentication;

public record ConfirmEmailRequest(string Token, string Email);
