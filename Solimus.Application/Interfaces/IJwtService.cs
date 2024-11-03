using Solimus.Domain.Entities;
using System.Security.Claims;

namespace Solimus.Application.Interfaces;

public interface IJwtService
{
    Task<string> GenerateJwtTokenAsync(SolimusUser user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
