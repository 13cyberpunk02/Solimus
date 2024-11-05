using Microsoft.AspNetCore.Identity;
using Solimus.Application.Common.Results;
using Solimus.Application.Error.RolesError;
using Solimus.Application.Interfaces;
using Solimus.Application.Models.Request.Role;
using Solimus.Application.Validators.Role;

namespace Solimus.Application.Services;

public class RoleService(RoleManager<IdentityRole> roleManager, UpdateRoleRequestValidator roleValidator) : IRoleService
{
    public async Task<Result> CreateRole(string name)
    {
        if (string.IsNullOrEmpty(name))
            return Result.Failure(RolesError.RoleNameEmpty);

        var isRoleExists = await roleManager.RoleExistsAsync(name);
        if (isRoleExists)
            return Result.Failure(RolesError.RoleExists);

        var result = await roleManager.CreateAsync(new IdentityRole
        {
            Name = name
        });

        if(!result.Succeeded)
            return Result.Failure(RolesError.ListOfErrors(result.Errors.Select(x => x.Description)));

        return Result.Success($"Роль {name} успешно создан");
    }

    public async Task<Result> DeleteRole(string roleId)
    {
        var roleToDelete = await roleManager.FindByIdAsync(roleId);
        if (roleToDelete is null)
            return Result.Failure(RolesError.RoleNotFound);
        var result = await  roleManager.DeleteAsync(roleToDelete);

        if (!result.Succeeded)
            return Result.Failure(RolesError.ListOfErrors(result.Errors.Select(x => x.Description)));

        return Result.Success($"Роль {roleToDelete.Name} удален");
    }

    public Result GetAllRoles()
    {
        var roles = roleManager.Roles;
        if(roles is null)
            return Result.Success("Нет ролей, создайте хотя бы одну");
        var result = roles.Select(x => new { Id = x.Id, Name = x.Name });
        return Result.Success(result);
    }

    public async Task<Result> GetRoleById(string roleId)
    {
        if(string.IsNullOrEmpty(roleId))
            return Result.Failure(RolesError.RoleIdEmpty);

        var role = await roleManager.FindByIdAsync(roleId);
        
        if(role is null)
            return Result.Failure(RolesError.RoleDoesntExist);
        return Result.Success(new {Id = role.Id, Name = role.Name });
    }

    public async Task<Result> GetRoleByName(string roleName)
    {
        if (string.IsNullOrEmpty(roleName))
            return Result.Failure(RolesError.RoleNameEmpty);

        var isRoleExists = await roleManager.RoleExistsAsync(roleName);
        if (!isRoleExists)
            return Result.Failure(RolesError.RoleDoesntExist);
        
        var role = await roleManager.FindByNameAsync(roleName);
        return Result.Success(new { Id = role.Id, Name = role.Name });
    }

    public async Task<Result> UpdateRole(UpdateRoleRequest request)
    {
        var isValidModel = await roleValidator.ValidateAsync(request);

        if (!isValidModel.IsValid)
            return Result.Failure(RolesError.ListOfErrors(isValidModel.Errors.Select(x => x.ErrorMessage)));

        var isRoleExists = await roleManager.RoleExistsAsync(request.Name);

        if (!isRoleExists)
            return Result.Failure(RolesError.RoleDoesntExist);
      
        var role = await roleManager.FindByNameAsync(request.Name);
        role.Name = request.Name;

        var result = await roleManager.UpdateAsync(role);

        if (!result.Succeeded)
            return Result.Failure(RolesError.ListOfErrors(result.Errors.Select(x => x.Description)));

        return Result.Success("Роль успешно обновлен");
    }
}
