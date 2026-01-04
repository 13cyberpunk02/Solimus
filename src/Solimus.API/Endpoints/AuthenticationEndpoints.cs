using Solimus.API.Common.Extensions;
using Solimus.Application.Authentication.DTO_s;
using Solimus.Application.Authentication.Service;

namespace Solimus.API.Endpoints;

public static class AuthenticationEndpoints
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("api/auth")
            .WithDisplayName("Authentication Endpoints")
            .WithTags("Authentication");

        group.MapPost("login", Login)
            .WithSummary("Login");
        return group;
    }

    private static async Task<IResult> Login(IAuthenticationService service, LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await service.Login(request, cancellationToken);
        return response.ToHttpResponse();
    }
}