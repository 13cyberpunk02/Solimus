using Carter;
using Solimus.API.Extensions;
using Solimus.Application.Interfaces;
using Solimus.Application.Models.Request.Authentication;

namespace Solimus.API.Endpoints;

public class AuthenticateEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("identity/");
        group.MapPost("registration", Registration);
        group.MapPost("login", Login);
        group.MapPut("confirm-email", ConfirmEmail);
        group.MapPost("forgot-password", ForgotPassword);
        group.MapPut("reset-password", ResetPassword);
        group.MapPut("refresh-token", RefreshToken);
    }

    private static async Task<IResult> Registration(RegistrationRequest request, IAuthenticationService authentication)
    {
        var response = await authentication.RegistrationAsync(request);

        return response.ToHttpResponse();
    }

    private static async Task<IResult> Login(LoginRequest request, IAuthenticationService authentication)
    {
        var response = await authentication.LoginAsync(request);

        return response.ToHttpResponse();
    }

    private static async Task<IResult> ConfirmEmail(ConfirmEmailRequest request, IAuthenticationService authenticationService)
    {
        var response = await authenticationService.ConfirmEmail(request);

        return response.ToHttpResponse();
    }

    private static async Task<IResult> ForgotPassword(string email, IAuthenticationService authenticationService)
    {
        var response = await authenticationService.ForgotPassword(email);

        return response.ToHttpResponse();
    }

    private static async Task<IResult> ResetPassword(ResetPasswordRequest request, IAuthenticationService authenticationService)
    {
        var response = await authenticationService.ResetPassword(request);

        return response.ToHttpResponse();
    }

    private static async Task<IResult> RefreshToken(RefreshTokenRequest request, IAuthenticationService authenticationService)
    {
        var response = await authenticationService.RefreshAccessToken(request);

        return response.ToHttpResponse();
    }
}
