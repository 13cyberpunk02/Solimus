using Carter;

namespace Solimus.API.Endpoints;

public class ChannelEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("channel/");

    }        
}
