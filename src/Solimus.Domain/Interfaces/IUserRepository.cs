using Solimus.Domain.Entities;

namespace Solimus.Domain.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default);
    Task<List<User>?> GetAllUsersWithRoles(CancellationToken cancellationToken = default);
}