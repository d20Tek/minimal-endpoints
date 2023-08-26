//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints;
using D20Tek.Minimal.Endpoints.Exceptions;

namespace D20Tek.HabitTracker.WebApi.Endpoints.Home;

public class HomeEndpoints : ICompositeApiEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup("/")
            .WithTags("Home")
            .WithOpenApi();

        groupBuilder.MapGet("/", GetHome);

        groupBuilder.MapGet("/throw-exception", () =>
        {
            throw new HttpResponseException(
                "Testing exception handler", StatusCodes.Status409Conflict);
        })
        .WithDisplayName("TestExceptionHandler")
        .ProducesProblem(StatusCodes.Status409Conflict);
    }

    internal string GetHome() => "Sample Web Api";
}
