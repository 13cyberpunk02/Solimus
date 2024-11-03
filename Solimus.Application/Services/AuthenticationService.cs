using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Solimus.Application.Common.Results;
using Solimus.Application.Error.AuthenticationErrors;
using Solimus.Application.Interfaces;
using Solimus.Application.Models.Options;
using Solimus.Application.Models.Request.Authentication;
using Solimus.Application.Validators.Authentication;
using Solimus.Domain.Entities;
using System.Text;

namespace Solimus.Application.Services;

public class AuthenticationService(    
    LoginRequestValidator loginValidator, 
    RegistrationRequestValidator registrationValidator,
    ConfirmEmailRequestValidator confirmEmailValidator,
    ResetPasswordRequestValidaor resetPasswordValidator,
    RefreshTokenRequestValidator refreshTokenValidator,
    IJwtService jwtService,
    UserManager<SolimusUser> userManager,
    IEmailService emailService,
    IConfiguration configuration) : IAuthenticationService
{
    public async Task<Result> ConfirmEmail(ConfirmEmailRequest confirmEmail)
    {
        var validModel = await confirmEmailValidator.ValidateAsync(confirmEmail);
        if (!validModel.IsValid)
            return Result.Failure(AuthenticationError.CreateInvalidLoginRequestError(validModel.Errors.Select(x => x.ErrorMessage)));

        var user = await userManager.FindByEmailAsync(confirmEmail.Email);
        if (user is null)
            return Result.Failure(AuthenticationError.UserNotFound);
        if(user.EmailConfirmed)
            return Result.Failure(AuthenticationError.EmailConfirmed);

        try
        {
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(confirmEmail.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

            var result = await userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
                return Result.Failure(AuthenticationError.InvalidTokenRequest);
            return Result.Success("Ваша электронная почта подтверждена. Вы можете авторизоваться.");
        }
        catch 
        {
            return Result.Failure(AuthenticationError.InvalidTokenRequest);
        }
    }

    public async Task<Result> ForgotPassword(string email)
    {
        if (string.IsNullOrEmpty(email))
            return Result.Failure(AuthenticationError.EmailEmptyOrNull);

        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return Result.Failure(AuthenticationError.UserNotFound);

        if (!user.EmailConfirmed)
            return Result.Failure(AuthenticationError.EmailNotConfirmed);

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var forgotPasswordOptions = new ForgotPasswordOptions(configuration, token);
        var emailSent = await emailService.SendForgotPasswordMail(new EmailOptions(configuration), forgotPasswordOptions, email, user.UserName!);

        if (!emailSent)
            return Result.Failure(AuthenticationError.EmailSendingFailure);

        return Result.Success("Письмо для смены пароля отправлено вам на почту.");
    }

    public async Task<Result> LoginAsync(LoginRequest loginRequest)
    {
        var validModel = await loginValidator.ValidateAsync(loginRequest);
        if (!validModel.IsValid)
            return Result.Failure(AuthenticationError.CreateInvalidLoginRequestError(validModel.Errors.Select(x => x.ErrorMessage)));

        var user = await userManager.FindByEmailAsync(loginRequest.Email);
        if (user is null) 
            return Result.Failure(AuthenticationError.UserNotFound);

        var isPasswordCorrect = await userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (!isPasswordCorrect)
            return Result.Failure(AuthenticationError.InvalidLoginRequest);

        var token = await jwtService.GenerateJwtTokenAsync(user);
        var refreshToken = jwtService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        var jwtConfig = new JwtConfig(configuration);
        user.RefreshTokenExpiryTime = DateTime.Now.AddHours(jwtConfig.RefreshTokenLifetimeInHour);
        
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return Result.Failure(AuthenticationError.CreateInvalidLoginRequestError(result.Errors.Select(x => x.Description)));

        return Result.Success(new { AccessToken = token, RefreshToken = refreshToken});
    }

    public async Task<Result> RefreshAccessToken(RefreshTokenRequest request)
    {
        var validModel = await refreshTokenValidator.ValidateAsync(request);
        if (!validModel.IsValid)
            return Result.Failure(AuthenticationError.CreateInvalidLoginRequestError(validModel.Errors.Select(x => x.ErrorMessage)));
        
        var user = await userManager.FindByEmailAsync(request.Email);
        if(user is null)
            return Result.Failure(AuthenticationError.UserNotFound);
        var principal = jwtService.GetPrincipalFromExpiredToken(request.Token);

        if (principal is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return Result.Failure(AuthenticationError.ErrorRequest);

        var newAccessToken = await jwtService.GenerateJwtTokenAsync(user);
        var newRefreshToken = jwtService.GenerateRefreshToken();

        var jwtConfig = new JwtConfig(configuration);
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddHours(jwtConfig.RefreshTokenLifetimeInHour);

        var result = await userManager.UpdateAsync(user);

        if(!result.Succeeded)
            return Result.Failure(AuthenticationError.CreateInvalidLoginRequestError(result.Errors.Select(x => x.Description)));
        return Result.Success(new { newAccessToken, newRefreshToken });
    }

    public async Task<Result> RegistrationAsync(RegistrationRequest registrationRequest)
    {
        var validModel = await registrationValidator.ValidateAsync(registrationRequest);
        if (!validModel.IsValid)
            return Result.Failure(AuthenticationError.CreateInvalidLoginRequestError(validModel.Errors.Select(x => x.ErrorMessage)));

        var isUserExists = await userManager.FindByEmailAsync(registrationRequest.Email);

        if (isUserExists is not null)
            return Result.Failure(AuthenticationError.UserAlreadyExists);

        var userToAdd = new SolimusUser
        {
            Email = registrationRequest.Email,
            UserName = registrationRequest.Username,
            JoinedDate = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(userToAdd, registrationRequest.Password);

        if (!result.Succeeded)
            return Result.Failure(AuthenticationError.CreateInvalidLoginRequestError(result.Errors.Select(x => x.Description)));

        var token = await userManager.GenerateEmailConfirmationTokenAsync(userToAdd);
        var emailConfirmOptions = new EmailRegistrationConfirmOptions(configuration, token);
        var isMailSend = await emailService.SendRegistrationConfirmationMailAsync(
            new EmailOptions(configuration),
            emailConfirmOptions,
            userToAdd.Email,
            userToAdd.UserName);

        if (!isMailSend)
            return Result.Failure(AuthenticationError.EmailSendingFailure);
        
        return Result.Success("Регистрация прошла успешно, перейдите на свою почту для подтверждения вашей учетной записи.");
    }

    public async Task<Result> ResetPassword(ResetPasswordRequest resetPasswordModel)
    {
        var validModel = await resetPasswordValidator.ValidateAsync(resetPasswordModel);
        if(!validModel.IsValid)
            return Result.Failure(AuthenticationError.CreateInvalidLoginRequestError(validModel.Errors.Select(x => x.ErrorMessage)));
        var user = await userManager.FindByEmailAsync(resetPasswordModel.Email);
        if(user is null)
            return Result.Failure(AuthenticationError.UserNotFound);

        if (!user.EmailConfirmed)
            return Result.Failure(AuthenticationError.EmailNotConfirmed);

        try
        {
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(resetPasswordModel.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

            var result = await userManager.ResetPasswordAsync(user, decodedToken, resetPasswordModel.NewPassword);

            if (!result.Succeeded)
                return Result.Failure(AuthenticationError.TokenUsed);

            await emailService.SendPasswordChangedMail(new EmailOptions(configuration), user.Email!, user.UserName!);

            return Result.Success("Пароль успешно был изменен.");
        }
        catch  
        {
            return Result.Failure(AuthenticationError.TokenUsed);
        }
    }
}
