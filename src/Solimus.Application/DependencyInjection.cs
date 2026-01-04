using Microsoft.Extensions.DependencyInjection;
using Solimus.Application.Authentication.Service;
using Solimus.Application.JWT;

namespace Solimus.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}