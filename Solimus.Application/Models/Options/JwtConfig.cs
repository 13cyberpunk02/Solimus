using Microsoft.Extensions.Configuration;

namespace Solimus.Application.Models.Options;

public class JwtConfig(IConfiguration configuration)
{
    public string Key { get; } = configuration["Jwt:Key"]!;
    public string Issuer { get; } = configuration["Jwt:Issuer"]!;
    public string Audience { get; } = configuration["Jwt:Audience"]!;
    public int ExpiresInHour  { get; } = int.Parse(configuration["Jwt:ExpiresInHour"]!);
    public int RefreshTokenLifetimeInHour { get; } = int.Parse(configuration["Jwt:RefreshTokenLifetimeInHour"]!);
}