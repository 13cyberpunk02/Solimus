using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Solimus.API.Common.Extensions;
using Solimus.API.Common.Filters;
using Solimus.Application.Authentication.DTO_s;
using Solimus.Application.Authentication.Service;
using LoginRequest = Solimus.Application.Authentication.DTO_s.LoginRequest;

namespace Solimus.API.Endpoints;

public static class AuthenticationEndpoints
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("api/auth")
            .WithDisplayName("Authentication Endpoints")
            .WithTags("Authentication")
            .AddEndpointFilter<RequestLoggingFilter>();

        group.MapPost("login", Login)
            .WithRequestValidation<LoginRequest>()
            .WithSummary("Login");
        
        group.MapPost("register", Registration)
            .WithRequestValidation<RegistrationRequest>()
            .WithSummary("Registration");
        
        group.MapPost("refresh-token", RefreshToken)
            .WithRequestValidation<RefreshTokenRequest>()
            .WithSummary("Refresh Token");
        
        group.MapPost("logout/{userId:guid}", Logout)
            .RequireAuthorization(options =>
                {
                    options.RequireClaim(ClaimTypes.NameIdentifier);   
                })
            .WithSummary("Logout");
        return group;
    }

    private static async Task<IResult> Login(IAuthenticationService service, LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await service.Login(request, cancellationToken);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> Registration(IAuthenticationService service, RegistrationRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await service.Register(request, cancellationToken);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> RefreshToken(IAuthenticationService service, RefreshTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await service.RefreshToken(request, cancellationToken);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> Logout(IAuthenticationService service,
        [FromRoute]Guid userId,
        HttpContext httpContext,
        CancellationToken cancellationToken = default)
    {
        var requestUserId =
            httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await service.Logout(Guid.Parse(requestUserId), userId, cancellationToken);
        return response.ToHttpResponse();
    }
}