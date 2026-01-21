using Solimus.Domain.Entities;

namespace Solimus.Domain.Interfaces;

public interface IUserRoleRepository : IGenericRepository<UserRole>
{
    Task<List<UserRole>> GetUserRolesByUserId(Guid userId, CancellationToken cancellationToken = default);
}