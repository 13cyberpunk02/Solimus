using Microsoft.EntityFrameworkCore;
using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Data.Context;

namespace Solimus.Infrastructure.Data.Repositories;

public class UserRepository(AppDbContext context) : 
    GenericRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default) => 
        await _set
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<List<User>?> GetAllUsersWithRoles(CancellationToken cancellationToken = default)
        => await _set
            .Include(u => u.UserRoles)
            .ThenInclude(x => x.Role)
            .ToListAsync(cancellationToken);
}