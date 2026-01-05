using Microsoft.EntityFrameworkCore;
using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Data.Context;

namespace Solimus.Infrastructure.Data.Repositories;

public class RoleRepository(AppDbContext context) : 
    GenericRepository<Role>(context), IRoleRepository
{
    public async Task<Role?> GetRoleByName(string name, CancellationToken cancellationToken = default) =>
        await _set
            .AsNoTracking()
            .FirstOrDefaultAsync(role => role.RoleName == name, cancellationToken);
}