using Microsoft.AspNetCore.Mvc;
using Solimus.API.Common.Extensions;
using Solimus.API.Common.Filters;
using Solimus.Application.Users.DTO_s;
using Solimus.Application.Users.Services;

namespace Solimus.API.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("api/users")
            .WithDisplayName("User Endpoints")
            .WithTags("User")
            .RequireAuthorization(options =>
            {
                options.RequireRole("Admin");
                options.RequireAuthenticatedUser();
            })
            .AddEndpointFilter<RequestLoggingFilter>();

        group.MapGet("/", GetAllUsers)
            .WithSummary("Users");;

        group.MapGet("/{userId:guid}", GetUserById)
            .WithSummary("Get by Id");
        
        group.MapGet("/email={email:required}", GetUserByEmail)
            .WithSummary("Get by Email");

        group.MapPost("/create-user", CreateUser)
            .WithRequestValidation<CreateUserRequest>()
            .WithSummary("Create new user");
        
        group.MapPut("/{userId:guid}/update-user", UpdateUser)
            .WithRequestValidation<UpdateUserRequest>()
            .WithSummary("Update user");
        
        group.MapPut("/{userId:guid}/ban-user", BanUser)
            .WithRequestValidation<BanUserRequest>()
            .WithSummary("Ban user");
        
        group.MapPut("/{userId:guid}/unban-user", UnbanUser)
            .WithSummary("Unban user");
        
        group.MapPut("/{userId:guid}/change-password", ChangePassword)
            .WithRequestValidation<ChangeUserPasswordRequest>()
            .WithSummary("Change password");
        
        group.MapPut("/{userId:guid}/assign-role", AssignOrRemoveRole)
            .WithRequestValidation<AssignOrRemoveRoleRequest>()
            .WithSummary("Assign or remove role");
        
        return group;
    }

    private static async Task<IResult> GetUserById(
        IUserService userService,
        [FromRoute] Guid userId,
        CancellationToken ct = default)
    {
        var response = await userService.GetUserById(userId, ct);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> GetUserByEmail(
        IUserService userService,
        [FromRoute] string email,
        CancellationToken ct = default)
    {
        var response = await userService.GetUserByEmail(email, ct);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> GetAllUsers(
        IUserService userService,
        CancellationToken ct = default)
    {
        var response = await userService.GetAllUsers(ct);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> CreateUser(
        IUserService userService,
        CreateUserRequest createUserRequest,
        CancellationToken ct = default)
    {
        var response = await userService.CreateUser(createUserRequest, ct);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> UpdateUser(
        IUserService userService,
        UpdateUserRequest updateUserRequest,
        [FromRoute]Guid userId,
        CancellationToken ct = default)
    {
        var response = await userService.UpdateUser(updateUserRequest, userId, ct);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> BanUser(
        IUserService userService,
        BanUserRequest banUserRequest,
        [FromRoute]Guid userId,
        CancellationToken ct = default)
    {
        var response = await userService.BanUser(banUserRequest, userId, ct);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> UnbanUser(
        IUserService userService,
        [FromRoute] Guid userId,
        CancellationToken ct = default)
    {
        var response = await userService.UnBanUser(userId, ct);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> ChangePassword(
        IUserService userService,
        ChangeUserPasswordRequest changeUserPasswordRequest,
        [FromRoute]Guid userId,
        CancellationToken ct = default)
    {
        var response = await userService.ChangePassword(changeUserPasswordRequest, userId, ct);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> AssignOrRemoveRole(
        IUserService userService,
        [FromRoute]Guid userId,
        [FromBody] AssignOrRemoveRoleRequest assignOrRemoveRoleRequest,
        CancellationToken ct = default)
    {
        var response = await userService.AssignOrRemoveRole(userId, assignOrRemoveRoleRequest, ct);
        return response.ToHttpResponse();
    }
}