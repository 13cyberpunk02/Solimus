using Microsoft.EntityFrameworkCore;
using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Data.Context;

namespace Solimus.Infrastructure.Data.Repositories;

public class RefreshTokenRepository(AppDbContext context) : 
    GenericRepository<RefreshToken>(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetRefreshTokenByUserId(Guid userId, CancellationToken cancellationToken = default) =>
        await _set.SingleOrDefaultAsync(rt => rt.User.UserId == userId, cancellationToken);
}