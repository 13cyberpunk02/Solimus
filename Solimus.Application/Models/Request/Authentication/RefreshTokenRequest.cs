namespace Solimus.Application.Models.Request.Authentication;

public record RefreshTokenRequest(string RefreshToken, string Token, string Email);

