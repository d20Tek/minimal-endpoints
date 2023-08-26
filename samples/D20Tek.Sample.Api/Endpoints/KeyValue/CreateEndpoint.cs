//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints;
using D20Tek.Sample.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace D20Tek.Sample.Api.Endpoints.KeyValue;

internal sealed class CreateEndpoint : IApiEndpoint<KeyValueRequest>
{
    private readonly IKeyValueRepository _repository;

    public CreateEndpoint(IKeyValueRepository repository)
    {
        _repository = repository;
    }

    public void MapRoute(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/key-value", HandleAsync)
            .WithName("CreateKeyValue")
            .Produces<KeyValueResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .WithTags("KeyValue Service")
            .WithOpenApi();
    }

    public async Task<IResult> HandleAsync(
        [FromBody] KeyValueRequest request,
        CancellationToken cancellation)
    {
        await Task.CompletedTask;
        ArgumentNullException.ThrowIfNull(nameof(request));

        if (_repository.Create(request.Key, request.Value))
        {
            var createdUri = $"/key-value/{request.Key}";
            return TypedResults.Created(createdUri, request.ToResponse());
        }

        return TypedResults.Conflict();
    }
}
