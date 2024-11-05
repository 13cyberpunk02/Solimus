using Carter;
using Solimus.API.Extensions;
using Solimus.Application.Interfaces;
using Solimus.Application.Models.Request.Role;

namespace Solimus.API.Endpoints
{
    public class RoleEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("role/");
            group.MapPost("create-role", CreateRole).RequireAuthorization();
            group.MapDelete("delete-role", DeleteRole).RequireAuthorization();
            group.MapGet("get-all-roles", GetAllRoles).RequireAuthorization();
            group.MapGet("get-role-by-id", GetRoleById).RequireAuthorization();
            group.MapGet("get-role-by-name", GetRoleByName).RequireAuthorization();
            group.MapPut("update-role", UpdateRole).RequireAuthorization();
        }

        private static async Task<IResult> CreateRole(string name, IRoleService roleService)
        {
            var response = await roleService.CreateRole(name);

            return response.ToHttpResponse();
        }

        private static async Task<IResult> DeleteRole(string roleId, IRoleService roleService)
        {
            var response = await roleService.DeleteRole(roleId);

            return response.ToHttpResponse();
        }

        private static IResult GetAllRoles(IRoleService roleService) => roleService.GetAllRoles().ToHttpResponse();

        private static async Task<IResult> GetRoleById(string roleId, IRoleService roleService)
        {
            var response = await roleService.GetRoleById(roleId);
            
            return response.ToHttpResponse();
        }

        private static async Task<IResult> GetRoleByName(string roleName, IRoleService roleService)
        {
            var response = await roleService.GetRoleByName(roleName);

            return response.ToHttpResponse();
        }

        private static async Task<IResult> UpdateRole(UpdateRoleRequest request, IRoleService roleService)
        {
            var response = await roleService.UpdateRole(request);

            return response.ToHttpResponse();
        }
    }
}
