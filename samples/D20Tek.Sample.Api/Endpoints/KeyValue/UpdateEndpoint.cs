//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints;
using D20Tek.Minimal.Endpoints.Configuration;
using D20Tek.Sample.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace D20Tek.Sample.Api.Endpoints.KeyValue;

internal sealed class UpdateEndpoint : IApiEndpoint<KeyValueRequest>
{
    private readonly IKeyValueRepository _repository;

    public UpdateEndpoint(IKeyValueRepository repository)
    {
        _repository = repository;
    }

    public void MapRoute(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPut(Configuration.Update.RoutePattern, HandleAsync)
            .WithConfiguration(Configuration.Update)
            .WithOpenApi();
    }

    public async Task<IResult> HandleAsync(
        [FromBody] KeyValueRequest request,
        CancellationToken cancellation)
    {
        await Task.CompletedTask;
        ArgumentNullException.ThrowIfNull(nameof(request));

        if (_repository.Update(request.Key, request.Value))
        {
            return TypedResults.Ok(request.ToResponse());
        }

        return TypedResults.NotFound();
    }
}
