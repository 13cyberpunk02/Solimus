using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Solimus.Domain.Interfaces;
using Solimus.Domain.Options;
using Solimus.Infrastructure.Data.Context;
using Solimus.Infrastructure.Data.Repositories;

namespace Solimus.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<DatabaseOption>? configureOptions = null)
    {
        services.Configure<DatabaseOption>(configuration.GetSection(DatabaseOption.SectionName));
        services.Configure<JwtOption>(configuration.GetSection(JwtOption.SectionName));

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var databaseOptions = serviceProvider.GetRequiredService<IOptionsMonitor<DatabaseOption>>().CurrentValue;
            options.UseNpgsql(databaseOptions.ConnectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}