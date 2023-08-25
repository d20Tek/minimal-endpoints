//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace D20Tek.Minimal.Endpoints;

public static class ApiEndpointExtensions
{
    public static IServiceCollection AddApiEndpoints(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped,
        bool includeInternalTypes = true)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        return AddApiEndpointsFromAssembly(services, assembly, lifetime, includeInternalTypes);
    }

    public static IServiceCollection AddApiEndpointsFromAssembly(
        this IServiceCollection services,
        Assembly assembly,
        ServiceLifetime lifetime = ServiceLifetime.Scoped,
        bool includeInternalTypes = true)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

        AddAssemblyTypes<ICompositeApiEndpoint>(services, assembly, lifetime, includeInternalTypes);
        AddAssemblyTypes<IApiEndpoint>(services, assembly, lifetime, includeInternalTypes);

        return services;
    }

    private static void AddAssemblyTypes<TInterface>(
        IServiceCollection services,
        Assembly assemblyToScan,
        ServiceLifetime lifetime,
        bool includeInternalTypes)
    {
        var interfaceType = typeof(TInterface);
        interfaceType.ThrowIfNotInterface();

        var endpointTypes = assemblyToScan.GetTypes()
            .Where(t =>
                !t.IsAbstract &&
                interfaceType.IsAssignableFrom(t) &&
                t != interfaceType);

        if (includeInternalTypes is false)
        {
            endpointTypes = endpointTypes.Where(t => t.IsPublic);
        }

        foreach (var type in endpointTypes)
        {
            var descriptor = new ServiceDescriptor(interfaceType, type, lifetime);
            services.Add(descriptor);
        }
    }

    public static IEndpointRouteBuilder MapApiEndpoints(
        this IEndpointRouteBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        using (var scope = builder.ServiceProvider.CreateScope())
        {
            MapCompositeEndpoints(builder, scope);
            MapApiEndpoints(builder, scope);
        }

        return builder;
    }

    private static void MapCompositeEndpoints(IEndpointRouteBuilder builder, IServiceScope scope)
    {
        // build collection of all ICompositeApiEndpoint services
        var composites = scope.ServiceProvider.GetServices<ICompositeApiEndpoint>();

        // loop through each ICompositeApiEndpoint implementations
        foreach (var item in composites)
        {
            // allow each endpoint to add its routes
            item.MapRoutes(builder);
        }
    }

    private static void MapApiEndpoints(IEndpointRouteBuilder builder, IServiceScope scope)
    {
        // build collection of all ICompositeApiEndpoint services
        var endpoints = scope.ServiceProvider.GetServices<IApiEndpoint>();

        // loop through each IApiEndpoint implementations
        foreach (var item in endpoints)
        {
            // allow each endpoint to add its routes
            item.MapRoute(builder);
        }
    }
}
