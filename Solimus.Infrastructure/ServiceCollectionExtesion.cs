using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Persistence.Context;
using Solimus.Infrastructure.Persistence.Repositories;

namespace Solimus.Infrastructure;

public static class ServiceCollectionExtesion
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string DatabaseConnectionString)
    {
        services.AddDbContext<SolimusContext>(options => options.UseSqlServer(DatabaseConnectionString));
        services.AddScoped<IUnitOfWork, IUnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IChannelRepository, ChannelRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISourceRepository, SourceRepository>();
        services.AddScoped<ILogotypeRepository, LogotypeRepository>();
        return services;
    }
}
