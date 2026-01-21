using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Solimus.Application.Authentication.Service;
using Solimus.Application.JWT;
using Solimus.Application.Role.Service;
using Solimus.Application.Users.Services;

namespace Solimus.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}