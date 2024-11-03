using Solimus.Application.Common.Results;
using Solimus.Application.Models.Request.Authentication;

namespace Solimus.Application.Interfaces;

public interface IAuthenticationService
{
    Task<Result> RegistrationAsync(RegistrationRequest registrationRequest);
    Task<Result> LoginAsync(LoginRequest loginRequest);
    Task<Result> ForgotPassword(string email);
    Task<Result> ConfirmEmail(ConfirmEmailRequest confirmEmail);
    Task<Result> ResetPassword(ResetPasswordRequest resetPasswordModel);
    Task<Result> RefreshAccessToken(RefreshTokenRequest refreshTokenRequest);
}
