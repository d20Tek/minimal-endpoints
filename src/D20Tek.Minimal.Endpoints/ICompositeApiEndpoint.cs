//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Routing;

namespace D20Tek.Minimal.Endpoints;

public interface ICompositeApiEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder);
}
