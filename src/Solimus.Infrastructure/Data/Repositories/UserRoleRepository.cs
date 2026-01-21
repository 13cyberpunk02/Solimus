using Microsoft.EntityFrameworkCore;
using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Data.Context;

namespace Solimus.Infrastructure.Data.Repositories;

public class UserRoleRepository(AppDbContext context) : 
    GenericRepository<UserRole>(context), IUserRoleRepository
{
    public async Task<List<UserRole>> GetUserRolesByUserId(Guid userId, CancellationToken cancellationToken = default)
        => await _set
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
}