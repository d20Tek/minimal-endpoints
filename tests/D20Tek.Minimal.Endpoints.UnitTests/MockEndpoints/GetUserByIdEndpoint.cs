using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace D20Tek.Minimal.Endpoints.UnitTests.MockEndpoints;

[ExcludeFromCodeCoverage]
public class GetUserByIdEndpoint : IApiEndpoint<GetByIdRequest>
{
    public Task<IResult> HandleAsync(GetByIdRequest request, CancellationToken cancellation) =>
        Task.FromResult(Results.Ok());

    public void MapRoute(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapGet("/entity", () => { });
}

[ExcludeFromCodeCoverage]
public record GetByIdRequest(string id) : IRequest<IResult>;
