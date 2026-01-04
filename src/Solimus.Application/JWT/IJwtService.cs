using Solimus.Domain.Entities;

namespace Solimus.Application.JWT;

public interface IJwtService
{
    public string GenerateJwtToken(User user);
    public string GenerateRefreshToken();
}