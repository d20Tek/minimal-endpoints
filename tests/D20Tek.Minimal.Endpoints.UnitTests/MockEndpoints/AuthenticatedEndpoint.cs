using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace D20Tek.Minimal.Endpoints.UnitTests.MockEndpoints;

[ExcludeFromCodeCoverage]
internal class AuthenticatedEndpoint : IAuthenticatedApiEndpoint<TestRequest>
{
    public Task<IResult> HandleAsync(TestRequest request, ClaimsPrincipal principal, CancellationToken cancellation) =>
        Task.FromResult(Results.Ok());

    public void MapRoute(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapGet("/auth", () => { });
}

[ExcludeFromCodeCoverage]
internal record TestRequest(string Name, string Value) : IRequest<IResult>;
