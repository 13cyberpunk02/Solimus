namespace Solimus.Application.Authentication.DTO_s;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);