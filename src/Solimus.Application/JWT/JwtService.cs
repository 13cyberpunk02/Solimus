using System.Globalization;
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
                new (JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new (JwtRegisteredClaimNames.Email, user.Email),
                new (JwtRegisteredClaimNames.Name, user.UserName),
                new(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenLifetimeInMinutes).ToString(CultureInfo.CurrentCulture))
            ];
        claims.AddRange(user.UserRoles.Select(ur => new Claim(ClaimTypes.Role, ur.Role.RoleName)));
        
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
    
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.PrivateKey)),
            ValidateLifetime = false 
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
            
        try
        {
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
                
            if (securityToken is not JwtSecurityToken jwtSecurityToken 
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return principal;
        }
        catch
        {
            return null;
        }
    }
}