namespace D20Tek.Minimal.Endpoints;

public static class ApiEndpointExtensions
{
    public static IServiceCollection AddApiEndpoints(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped,
        bool includeInternalTypes = true) =>
        services.AddApiEndpointsFromAssembly(Assembly.GetCallingAssembly(), lifetime, includeInternalTypes);

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
            .Where(t => !t.IsAbstract && interfaceType.IsAssignableFrom(t) && t != interfaceType);

        endpointTypes = includeInternalTypes ? endpointTypes : endpointTypes.Where(t => t.IsPublic);

        foreach (var type in endpointTypes)
        {
            var descriptor = new ServiceDescriptor(interfaceType, type, lifetime);
            services.Add(descriptor);
        }
    }

    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        using (var scope = builder.ServiceProvider.CreateScope())
        {
            builder.MapCompositeEndpoints(scope);
            builder.MapApiEndpoints(scope);
        }

        return builder;
    }

    private static void MapCompositeEndpoints(this IEndpointRouteBuilder builder, IServiceScope scope)
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

    private static void MapApiEndpoints(this IEndpointRouteBuilder builder, IServiceScope scope)
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
