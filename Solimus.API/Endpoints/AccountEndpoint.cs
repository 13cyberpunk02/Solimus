using Carter;
using Solimus.API.Extensions;
using Solimus.Application.Interfaces;

namespace Solimus.API.Endpoints;

public class AccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("account/");
        group.MapDelete("delete-user", DeleteUser).RequireAuthorization();
        group.MapGet("get-all-users", GetAllUsers).RequireAuthorization();
        group.MapGet("get-user-by-id", GetUserById).RequireAuthorization();
    }

    private static async Task<IResult> DeleteUser(string userId, IAccountService accountService)
    {
        var response = await accountService.DeleteUser(userId);

        return response.ToHttpResponse();
    }

    private static IResult GetAllUsers(IAccountService accountService)
    {
        var response = accountService.GetAllUsers();

        return response.ToHttpResponse();
    }

    private static async Task<IResult> GetUserById(string id, IAccountService accountService)
    {
        var response = await accountService.GetUserById(id);

        return response.ToHttpResponse();
    }
}
