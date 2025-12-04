using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace D20Tek.Minimal.Endpoints.UnitTests.MockEndpoints;

public class CompositeEndpoint : ICompositeApiEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder) =>
        routeBuilder.MapGet("/", [ExcludeFromCodeCoverage] () => { });
}
