using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Solimus.Application.Interfaces;
using Solimus.Application.Services;
using Solimus.Domain.Interfaces;

namespace Solimus.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);        
        services.AddScoped<IJwtService, JwtService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IChannelService, ChannelService>();
        return services;
    }
}
