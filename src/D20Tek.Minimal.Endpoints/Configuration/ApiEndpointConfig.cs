//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
namespace D20Tek.Minimal.Endpoints.Configuration;

public sealed record ApiEndpointConfig(
    string RoutePattern,
    string EndpointName,
    string? DisplayName = null,
    string[]? Tags = null,
    string? Summary = null,
    string? Description = null,
    bool RequiresAuthorization = false,
    string[]? AuthorizationPolicies = null,
    EndpointProducesConfig[]? Produces = null,
    EndpointAcceptsConfig[]? Accepts = null);
