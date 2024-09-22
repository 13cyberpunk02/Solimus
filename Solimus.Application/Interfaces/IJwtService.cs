using Solimus.Domain.Entities;

namespace Solimus.Application.Interfaces;

public interface IJwtService
{
    Task<string> GenerateJwtTokenAsync(SolimusUser user);
}
