//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace D20Tek.Minimal.Endpoints.Configuration;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithConfiguration(
        this RouteHandlerBuilder builder,
        ApiEndpointConfig config)
    {
        builder.ConfigureBasicMetadata(config)
               .ConfigureAuthMetadata(config)
               .ConfigureAcceptsMetadata(config)
               .ConfigureProducesMetadata(config);

        return builder;
    }

    private static RouteHandlerBuilder ConfigureBasicMetadata(
        this RouteHandlerBuilder builder,
        ApiEndpointConfig config)
    {
        builder.WithName(config.EndpointName);

        if (config.DisplayName is not null)
        {
            builder.WithDisplayName(config.DisplayName);
        }

        if (config.Tags is not null)
        {
            builder.WithTags(config.Tags);
        }

        if (config.Summary is not null)
        {
            builder.WithSummary(config.Summary);
        }

        if (config.Description is not null)
        {
            builder.WithDescription(config.Description);
        }

        return builder;
    }

    private static RouteHandlerBuilder ConfigureAuthMetadata(
        this RouteHandlerBuilder builder,
        ApiEndpointConfig config)
    {
        if (config.RequiresAuthorization is true)
        {
            if (config.AuthorizationPolicies is not null)
            {
                builder.RequireAuthorization(config.AuthorizationPolicies);
            }
            else
            {
                builder.RequireAuthorization();
            }
        }

        return builder;
    }

    private static RouteHandlerBuilder ConfigureProducesMetadata(
        this RouteHandlerBuilder builder,
        ApiEndpointConfig config)
    {
        if (config.Produces is not null)
        {
            foreach (var item in config.Produces)
            {
                builder.Produces(
                    item.StatusCode,
                    item.ResponseType,
                    item.ContentType,
                    item.AdditionalContentTypes ?? Array.Empty<string>());
            }
        }
        return builder;
    }

    private static RouteHandlerBuilder ConfigureAcceptsMetadata(
        this RouteHandlerBuilder builder,
        ApiEndpointConfig config)
    {
        if (config.Accepts is not null)
        {
            foreach (var item in config.Accepts)
            {
                builder.Accepts(
                    item.RequestType,
                    item.IsOptional,
                    item.ContentType,
                    item.AdditionalContentTypes ?? Array.Empty<string>());
            }
        }
        return builder;
    }
}
