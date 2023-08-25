//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics.CodeAnalysis;

namespace D20Tek.Minimal.Endpoints.UnitTests.MockEndpoints;

[ExcludeFromCodeCoverage]
public class GetUserByIdEndpoint : IApiEndpoint<GetByIdRequest>
{
    public Task<IResult> HandleAsync(GetByIdRequest request, CancellationToken cancellation)
    {
        return Task.FromResult(Results.Ok());
    }

    public void MapRoute(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/entity", () => { });
    }
}

[ExcludeFromCodeCoverage]
public record GetByIdRequest(string id) : IRequest<IResult>;
