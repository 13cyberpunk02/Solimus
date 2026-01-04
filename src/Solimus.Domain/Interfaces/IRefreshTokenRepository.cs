using Solimus.Domain.Entities;

namespace Solimus.Domain.Interfaces;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<RefreshToken?> GetRefreshTokenByUserId(Guid userId, CancellationToken cancellationToken = default);
}