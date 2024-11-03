using Microsoft.Extensions.Configuration;

namespace Solimus.Application.Models.Options;

public class ForgotPasswordOptions(IConfiguration configuration, string token)
{
    public string Token { get; } = token;
    public string ClientAppUrl { get; } = configuration["AccountEmailOptions:ClientAppUrl"]!;
    public string ResetPasswordPath { get; } = configuration["AccountEmailOptions:ResetPasswordPath"]!;
}
