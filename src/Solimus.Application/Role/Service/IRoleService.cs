using Solimus.Application.Common;
using Solimus.Application.Role.DTO_s;

namespace Solimus.Application.Role.Service;

public interface IRoleService
{
    Task<Result<List<GetRoleResponse>>> GetRoles(CancellationToken ct = default);
    Task<Result<GetRoleResponse>> GetRoleById(Guid roleId, CancellationToken ct = default);
    Task<Result<GetRoleResponse>> GetRoleByName(string roleName, CancellationToken ct = default);
}