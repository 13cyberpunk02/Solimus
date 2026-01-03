using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Data.Context;

namespace Solimus.Infrastructure.Data.Repositories;

public class RoleRepository(AppDbContext context) : 
    GenericRepository<Role>(context), IRoleRepository
{
}