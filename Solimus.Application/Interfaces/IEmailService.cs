using Solimus.Application.Models.Options;

namespace Solimus.Application.Interfaces;

public interface IEmailService
{
    Task<bool> SendRegistrationConfirmationMailAsync(EmailOptions emailOptions, EmailRegistrationConfirmOptions emailRegistrationConfirmOptions,
        string toEmail, string userName);

    Task<bool> SendForgotPasswordMail(EmailOptions emailOptions, ForgotPasswordOptions forgotPasswordOptions,
    string toEmail, string userName);

    Task<bool> SendPasswordChangedMail(EmailOptions emailOptions, string toEmail, string userName);
}
