using Microsoft.AspNetCore.WebUtilities;
using Solimus.Application.Interfaces;
using Solimus.Application.Models.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Solimus.Application.Services;

public class EmailService : IEmailService
{
    private Task SendEmailAsync(EmailOptions emailOptions, string toEmail, string subject, string body, bool isBodyHtml = false)
    {
        var client = new SmtpClient(emailOptions.MailServer, emailOptions.Port)
        {
            Credentials = new NetworkCredential(emailOptions.FromEmail, emailOptions.PrivateKey),
            EnableSsl = true
        };

        MailMessage mailMessage = new MailMessage(emailOptions.FromEmail, toEmail, subject, body)
        {
            IsBodyHtml = isBodyHtml
        };

        return client.SendMailAsync(mailMessage);
    }

    public async Task<bool> SendForgotPasswordMail(EmailOptions emailOptions, ForgotPasswordOptions forgotPasswordOptions, string toEmail, string userName)
    {
        var token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(forgotPasswordOptions.Token));
        var url = $"{forgotPasswordOptions.ClientAppUrl}/{forgotPasswordOptions.ResetPasswordPath}?token={token}&email={toEmail}";
        var body = $"<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Здравствуйте, {userName}</p>" +
                   $"<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Имя пользователя: {toEmail}</p>" +
                   "<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Для того, чтобы сбросить пароль нажмите на кнопку ниже</p>" +
                   $"<p><a href=\"{url}\" style=\"display: inline-block; padding: 10px 20px; font-family: Arial, sans-serif; font-size: 16px; color: white; background-color: #1a73e8; text-decoration: none; border-radius: 5px;\">Нажмите сюда для сброса пароля.</a></p>" +
                   "<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Спасибо, что выбрали наш сервис!</p>" +
                   "<br><p style=\"font-family: Arial, sans-serif; font-size: 18px; font-weight: bold; font-style: italic; color: #333;\">Solimus</p>";
        try
        {
            await SendEmailAsync(emailOptions, toEmail, "Восстановление пароля", body, true);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> SendPasswordChangedMail(EmailOptions emailOptions, string toEmail, string userName)
    {
        var body = $"<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Уведомление для {userName}</p>" +
                   $"<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Для учётной записи: {toEmail}</p>" +
                   "<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Был изменен пароль</p>" +
                   "<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Если это были не Вы, то просим вас немедленно обратиться в службу поддержки.</p>" +
                   "<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Спасибо, что выбрали наш сервис!</p>" +
                   "<br><p style=\"font-family: Arial, sans-serif; font-size: 18px; font-weight: bold; font-style: italic; color: #333;\">Solimus</p>";

        try
        {
            await SendEmailAsync(emailOptions, toEmail, "Изменение пароля", body, true);
            return true;
        }
        catch 
        {
            return false;
        }
    }

    public async Task<bool> SendRegistrationConfirmationMailAsync(EmailOptions emailOptions, EmailRegistrationConfirmOptions emailRegistrationConfirmOptions, string toEmail, string userName)
    {
        var token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailRegistrationConfirmOptions.Token));
        var url = $"{emailRegistrationConfirmOptions.ClientAppUrl}/{emailRegistrationConfirmOptions.ConfirmEmailPath}?token={token}&email={toEmail}";
        var body = $"<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Здравствуйте, {userName}</p>" +
           "<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Пожалуйста, подтвердите вашу учетную запись, нажав на кнопку ниже:</p>" +
           $"<p><a href=\"{url}\" style=\"display: inline-block; padding: 10px 20px; font-family: Arial, sans-serif; font-size: 16px; color: white; background-color: #1a73e8; text-decoration: none; border-radius: 5px;\">Нажмите для подтверждения</a></p>" +
           "<p style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333;\">Спасибо, что выбрали наш сервис!</p>" +
           "<br><p style=\"font-family: Arial, sans-serif; font-size: 18px; font-weight: bold; font-style: italic; color: #333;\">Solimus</p>";


        try
        {
            await SendEmailAsync(emailOptions, toEmail, "Регистрация", body, true);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
