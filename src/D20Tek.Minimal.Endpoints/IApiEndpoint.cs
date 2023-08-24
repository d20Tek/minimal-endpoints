//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace D20Tek.Minimal.Endpoints;

public interface IApiEndpoint
{
    public void MapRoute(IEndpointRouteBuilder routeBuilder);
}

public interface IApiEndpoint<TRequest> : IApiEndpoint
    where TRequest : IRequest<IResult>
{
    public Task<IResult> HandleAsync(TRequest request, CancellationToken cancellation);
}

public interface IAuthenticatedApiEndpoint<TRequest> : IApiEndpoint
    where TRequest : IRequest<IResult>
{
    public Task<IResult> HandleAsync(
        TRequest request,
        ClaimsPrincipal principal,
        CancellationToken cancellation);
}
