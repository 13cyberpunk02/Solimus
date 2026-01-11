using Solimus.API.Endpoints;

namespace Solimus.API.Common.Extensions;

public static class EndpointsMappingExtension
{
    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapAuthenticationEndpoints();
        endpoints.MapUserEndpoints();
        return endpoints;
    }
}