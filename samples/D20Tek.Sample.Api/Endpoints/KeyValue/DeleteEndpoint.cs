//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints;
using D20Tek.Minimal.Endpoints.Configuration;
using D20Tek.Sample.Api.Services;

namespace D20Tek.Sample.Api.Endpoints.KeyValue;

internal sealed class DeleteEndpoint : IApiEndpoint<KeyRequest>
{
    private readonly IKeyValueRepository _repository;

    public DeleteEndpoint(IKeyValueRepository repository)
    {
        _repository = repository;
    }

    public void MapRoute(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapDelete(Configuration.Delete.RoutePattern, HandleAsync)
            .WithConfiguration(Configuration.Delete)
            .WithOpenApi();
    }

    public async Task<IResult> HandleAsync(
        [AsParameters] KeyRequest request,
        CancellationToken cancellation)
    {
        await Task.CompletedTask;
        ArgumentNullException.ThrowIfNull(nameof(request));

        if (_repository.Delete(request.Key))
        {
            return TypedResults.Ok(request);
        }

        return TypedResults.NotFound();
    }
}
