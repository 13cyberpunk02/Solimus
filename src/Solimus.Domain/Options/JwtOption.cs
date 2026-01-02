namespace Solimus.Domain.Options;

public class JwtOption
{
    public string PrivateKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenLifetimeInMinutes { get; set; } = 15;
    public int RefreshTokenLifetimeInDays { get; set; } = 7;
}