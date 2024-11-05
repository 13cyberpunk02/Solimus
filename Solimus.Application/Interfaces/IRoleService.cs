using Solimus.Application.Common.Results;
using Solimus.Application.Models.Request.Role;

namespace Solimus.Application.Interfaces;

public interface IRoleService
{
    Result GetAllRoles();
    Task<Result> GetRoleById(string roleId);
    Task<Result> GetRoleByName(string roleName);
    Task<Result> CreateRole(string name);
    Task<Result> DeleteRole(string roleId);
    Task<Result> UpdateRole(UpdateRoleRequest request);
}
