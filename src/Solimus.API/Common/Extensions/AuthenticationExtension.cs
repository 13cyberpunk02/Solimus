using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Solimus.Domain.Options;

namespace Solimus.API.Common.Extensions;

public static class AuthenticationExtension
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<JwtOption>(
            configuration.GetSection(JwtOption.SectionName));

        var jwtOption = new JwtOption();
        configuration.GetSection(JwtOption.SectionName).Bind(jwtOption);

        if (string.IsNullOrEmpty(jwtOption.PrivateKey))
            throw new InvalidOperationException("JWT приватный ключ не указано, проверьте переменные окружения");
        
        if (jwtOption.PrivateKey.Length < 32)
            throw new InvalidOperationException("JWT приватный ключ должен содержать хотя бы 32 символа");

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = jwtOption.Issuer,
                    ValidAudience = jwtOption.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOption.PrivateKey))
                };
            });
        services.AddAuthorization();
        return services;
    }
}