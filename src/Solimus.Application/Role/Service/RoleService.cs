using Solimus.Application.Common;
using Solimus.Application.Role.DTO_s;
using Solimus.Domain.Interfaces;

namespace Solimus.Application.Role.Service;

public class RoleService(IUnitOfWork unitOfWork) : IRoleService
{
    private readonly IRoleRepository _roleRepository = unitOfWork.Roles;

    public async Task<Result<List<GetRoleResponse>>> GetRoles(CancellationToken ct = default)
    {
        var roles = await _roleRepository.GetAllAsync(ct);
        return Result<List<GetRoleResponse>>.Success(roles.Select(role =>
            new GetRoleResponse(RoleId: role.RoleId, RoleName: role.RoleName))
            .ToList());
    }

    public async Task<Result<GetRoleResponse>> GetRoleById(Guid roleId, CancellationToken ct = default)
    {
        var role = await _roleRepository.GetByIdAsync(roleId, ct);
        return Result<GetRoleResponse>.Success(new GetRoleResponse(RoleId: role.RoleId, RoleName: role.RoleName));
    }

    public async Task<Result<GetRoleResponse>> GetRoleByName(string roleName, CancellationToken ct = default)
    {
        var role = await _roleRepository.GetRoleByName(roleName, ct);
        return Result<GetRoleResponse>.Success(new GetRoleResponse(RoleId: role.RoleId, RoleName: role.RoleName));
    }
}