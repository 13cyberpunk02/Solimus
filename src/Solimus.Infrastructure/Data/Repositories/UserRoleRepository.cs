using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Data.Context;

namespace Solimus.Infrastructure.Data.Repositories;

public class UserRoleRepository(AppDbContext context) : 
    GenericRepository<UserRole>(context), IUserRoleRepository
{
}