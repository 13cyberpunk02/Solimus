using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Solimus.Domain.Entities;
using Solimus.Domain.Options;

namespace Solimus.Application.JWT;

public class JwtService(IOptionsMonitor<JwtOption> jwtOptions) : IJwtService
{
    private readonly JwtOption _jwtOptions = jwtOptions.CurrentValue;
    public string GenerateJwtToken(User user)
    {
        List<Claim> claims =
            [
                new (JwtRegisteredClaimNames.Sub, user.UserName),
                new (JwtRegisteredClaimNames.Email, user.Email),
                new (JwtRegisteredClaimNames.Jti, Guid.CreateVersion7().ToString())
            ];

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.PrivateKey));
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenLifetimeInMinutes),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}