namespace D20Tek.Sample.Api.Endpoints.Home;

public sealed class HomeEndpoints : ICompositeApiEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup("/")
                                       .WithTags("Home")
                                       .WithOpenApi();

        groupBuilder.MapGet("/", GetHome);

        groupBuilder.MapGet("/throw-exception", () =>
        {
            throw new HttpResponseException("Testing exception handler", StatusCodes.Status409Conflict);
        })
        .WithDisplayName("TestExceptionHandler")
        .ProducesProblem(StatusCodes.Status409Conflict);
    }

    internal string GetHome() => "Sample Web Api";
}
