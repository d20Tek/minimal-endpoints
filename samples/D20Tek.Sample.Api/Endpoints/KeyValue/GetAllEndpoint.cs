//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints;
using D20Tek.Sample.Api.Services;

namespace D20Tek.Sample.Api.Endpoints.KeyValue;

internal sealed class GetAllEndpoint : IApiEndpoint<EmptyRequest>
{
    private readonly IKeyValueRepository _repository;

    public GetAllEndpoint(IKeyValueRepository repository)
    {
        _repository = repository;
    }

    public void MapRoute(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/key-value", HandleAsync)
            .WithName("GetAll")
            .Produces<KeyValueResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("KeyValue Service")
            .WithOpenApi();
    }

    public async Task<IResult> HandleAsync(
        [AsParameters] EmptyRequest request,
        CancellationToken cancellation)
    {
        await Task.CompletedTask;
        ArgumentNullException.ThrowIfNull(nameof(request));

        var results = _repository.Get();
        if (results.Any())
        {
            var responses = results.Select(kv => new KeyValueResponse(kv.Key, kv.Value))
                .ToList();

            return TypedResults.Ok(responses);
        }

        return TypedResults.NoContent();
    }
}
