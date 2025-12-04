namespace D20Tek.Minimal.Endpoints.Configuration;

public static class Config
{
    public static EndpointAcceptsConfig Accepts<T>(
        string contentType,
        bool isOptional = false,
        string[]? additionalContentTypes = null) =>
        new(typeof(T), contentType, isOptional, additionalContentTypes);

    public static EndpointAcceptsConfig Accepts(
        Type requestType,
        string contentType,
        bool isOptional = false,
        string[]? additionalContentTypes = null) =>
        new(requestType, contentType, isOptional, additionalContentTypes);

    public static EndpointProducesConfig Produces(
        int statusCode,
        string? contentType = null,
        string[]? additionalContentTypes = null) => 
        new(statusCode, null, contentType, additionalContentTypes);

    public static EndpointProducesConfig Produces<T>(
        int statusCode,
        string? contentType = null,
        string[]? additionalContentTypes = null) =>
        new(statusCode, typeof(T), contentType, additionalContentTypes);

    public static EndpointProducesConfig ProducesProblem(int statusCode, string? contentType = null) =>
        new(statusCode, typeof(ProblemDetails), contentType);

    public static EndpointProducesConfig ProducesValidationProblem(int statusCode = 400, string? contentType = null) =>
        new(statusCode, typeof(HttpValidationProblemDetails), contentType);
}
