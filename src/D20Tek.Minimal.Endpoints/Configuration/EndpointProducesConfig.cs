namespace D20Tek.Minimal.Endpoints.Configuration;

public sealed record EndpointProducesConfig(
    int StatusCode,
    Type? ResponseType = null,
    string? ContentType = null,
    string[]? AdditionalContentTypes = null);
