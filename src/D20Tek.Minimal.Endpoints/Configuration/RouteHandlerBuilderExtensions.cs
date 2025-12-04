namespace D20Tek.Minimal.Endpoints.Configuration;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithConfiguration(this RouteHandlerBuilder builder, ApiEndpointConfig config) =>
        builder.ConfigureBasicMetadata(config)
               .ConfigureAuthMetadata(config)
               .ConfigureAcceptsMetadata(config)
               .ConfigureProducesMetadata(config);

    private static RouteHandlerBuilder ConfigureBasicMetadata(
        this RouteHandlerBuilder builder,
        ApiEndpointConfig config) =>
        builder.WithName(config.EndpointName)
               .ApplyIfNotNull(config.DisplayName, (b, v) => b.WithDisplayName(v))
               .ApplyIfNotNull(config.Tags, (b, v) => b.WithTags(v))
               .ApplyIfNotNull(config.Summary, (b, v) => b.WithSummary(v))
               .ApplyIfNotNull(config.Description, (b, v) => b.WithDescription(v));

    public static RouteHandlerBuilder ApplyIfNotNull<TValue>(
        this RouteHandlerBuilder target,
        TValue? value,
        Action<RouteHandlerBuilder, TValue> apply)
    {
        if (value is not null) apply(target, value);
        return target;
    }

    private static RouteHandlerBuilder ConfigureAuthMetadata(
        this RouteHandlerBuilder builder,
        ApiEndpointConfig config) =>
        config.RequiresAuthorization switch
        {
            true when config.AuthorizationPolicies is { Length: > 0 } =>
                builder.RequireAuthorization(config.AuthorizationPolicies),
            true => builder.RequireAuthorization(),
            false => builder.AllowAnonymous(),
        };

    private static RouteHandlerBuilder ConfigureProducesMetadata(
        this RouteHandlerBuilder builder,
        ApiEndpointConfig config) =>
        (config.Produces ?? []).Aggregate(builder, static (b, i) =>
            b.Produces(i.StatusCode, i.ResponseType, i.ContentType, i.AdditionalContentTypes ?? []));

    private static RouteHandlerBuilder ConfigureAcceptsMetadata(
        this RouteHandlerBuilder builder,
        ApiEndpointConfig config) =>
        (config.Accepts ?? []).Aggregate(builder, static (b, i) =>
            b.Accepts(i.RequestType, i.IsOptional, i.ContentType, i.AdditionalContentTypes ?? []));
}
