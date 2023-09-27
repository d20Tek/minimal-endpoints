//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints.Configuration;

namespace D20Tek.Sample.Api.Endpoints.KeyValue;

public static class Configuration
{
    private static string[] _keyValueTags = new[] { "KeyValue Service" };
    private const string _keyValueRoute = "/key-value";

    public static ApiEndpointConfig Create = new(
        _keyValueRoute,
        "CreateKeyValue",
        "Create KeyValue",
        Tags: _keyValueTags,
        Produces: new []
        {
            Config.Produces<KeyValueResponse>(StatusCodes.Status201Created),
            Config.ProducesProblem(StatusCodes.Status409Conflict)
        });

    public static ApiEndpointConfig Delete = new(
        $"{_keyValueRoute}/{{key}}",
        "DeleteKeyValue",
        "Delete KeyValue",
        Tags: _keyValueTags,
        Produces: new[]
        {
            Config.Produces<KeyValueResponse>(StatusCodes.Status200OK),
            Config.ProducesProblem(StatusCodes.Status404NotFound)
        });

    public static ApiEndpointConfig GetAll = new(
        _keyValueRoute,
        "GetAll",
        "GetAll KeyValues",
        Tags: _keyValueTags,
        Produces: new[]
        {
            Config.Produces<KeyValueResponse>(StatusCodes.Status200OK),
            Config.Produces(StatusCodes.Status204NoContent)
        });

    public static ApiEndpointConfig GetByKey = new(
        $"{_keyValueRoute}/{{key}}",
        "GetByKey",
        "Get By Key",
        Tags: _keyValueTags,
        Produces: new[]
        {
            Config.Produces<KeyValueResponse>(StatusCodes.Status200OK),
            Config.ProducesProblem(StatusCodes.Status404NotFound)
        });

    public static ApiEndpointConfig Update = new(
        $"{_keyValueRoute}/{{key}}",
        "UpdateKeyValue",
        "Update KeyValue",
        Tags: _keyValueTags,
        Produces: new[]
        {
            Config.Produces<KeyValueResponse>(StatusCodes.Status200OK),
            Config.ProducesProblem(StatusCodes.Status404NotFound)
        });
}
