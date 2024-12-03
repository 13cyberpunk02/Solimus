using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Persistence.Repositories;

namespace Solimus.API.Extensions;

public static class EntityExtensions
{
    public static IServiceCollection AddEntityServices(this IServiceCollection services)
    {
        services.AddScoped<IChannelRepository, ChannelRepository>();
        return services;
    }
}