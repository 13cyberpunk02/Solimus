using Microsoft.AspNetCore.Mvc;
using Solimus.API.Common.Extensions;
using Solimus.API.Common.Filters;
using Solimus.Application.Role.Service;

namespace Solimus.API.Endpoints;

public static class RoleEndpoint
{
    public static IEndpointRouteBuilder MapRoleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("api/roles")
            .WithDisplayName("Role Endpoints")
            .WithTags("Roles")
            .RequireAuthorization(options =>
            {
                options.RequireRole("Admin");
                options.RequireAuthenticatedUser();
            })
            .AddEndpointFilter<RequestLoggingFilter>();

        group.MapGet("/", GetRoles)
            .WithSummary("Get all roles");
        
        group.MapGet("/{roleId:guid}", GetRoleById)
            .WithSummary("Get role by id");
        
        group.MapGet("/role-name={roleName:required}", GetRoleByName)
            .WithSummary("Get role by name");
        
        return group;
    }

    private static async Task<IResult> GetRoles(IRoleService roleService, CancellationToken ct)
    {
        var response = await roleService.GetRoles(ct);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> GetRoleById(
        IRoleService roleService,
        [FromRoute] Guid roleId,
        CancellationToken ct)
    {
        var response = await roleService.GetRoleById(roleId, ct);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> GetRoleByName(
        IRoleService roleService,
        [FromRoute]string roleName,
        CancellationToken ct
    )
    {
        var response = await roleService.GetRoleByName(roleName, ct);
        return response.ToHttpResponse();
    }
}