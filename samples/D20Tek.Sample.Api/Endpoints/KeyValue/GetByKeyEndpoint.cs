﻿//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints;
using D20Tek.Minimal.Endpoints.Configuration;
using D20Tek.Sample.Api.Services;

namespace D20Tek.Sample.Api.Endpoints.KeyValue;

internal sealed class GetByKeyEndpoint : IApiEndpoint<KeyRequest>
{
    private readonly IKeyValueRepository _repository;

    public GetByKeyEndpoint(IKeyValueRepository repository)
    {
        _repository = repository;
    }

    public void MapRoute(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Configuration.GetByKey.RoutePattern, HandleAsync)
            .WithConfiguration(Configuration.GetByKey)
            .WithOpenApi();
    }

    public async Task<IResult> HandleAsync(
        [AsParameters] KeyRequest request,
        CancellationToken cancellation)
    {
        await Task.CompletedTask;
        ArgumentNullException.ThrowIfNull(nameof(request));

        var result = _repository.Get(request.Key);
        if (result is not null)
        {
            var kv = result.Value;
            return TypedResults.Ok(new KeyValueResponse(kv.Key, kv.Value));
        }

        return TypedResults.NotFound();
    }
}
