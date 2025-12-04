namespace D20Tek.Minimal.Endpoints.Configuration;

public sealed record EndpointAcceptsConfig(
    Type RequestType,
    string ContentType,
    bool IsOptional = false,
    string[]? AdditionalContentTypes = null);
