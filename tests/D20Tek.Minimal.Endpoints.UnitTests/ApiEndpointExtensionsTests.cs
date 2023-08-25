//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace D20Tek.Minimal.Endpoints.UnitTests;

[TestClass]
public class ApiEndpointExtensionsTests
{
    [TestMethod]
    public void AddApiEndpoints_AssemblyWithNoEndpoints_ReturnsEmptyServices()
    {
        // arrange
        IServiceCollection services = new ServiceCollection();

        // act
        services = services.AddApiEndpoints();

        // assert
        services.Should().BeEmpty();
    }

    [TestMethod]
    public void AddApiEndpointsFromAssembly_WithScopedInternals_ReturnsServices()
    {
        // arrange
        IServiceCollection services = new ServiceCollection();
        var assembly = this.GetType().Assembly;

        // act
        services = services.AddApiEndpointsFromAssembly(assembly);

        // assert
        services.Should().NotBeEmpty();
        services.Should().HaveCount(3);
        services.Count((descriptor) => descriptor.Lifetime == ServiceLifetime.Scoped)
                .Should().Be(3);
        services.Count((descriptor) => descriptor.Lifetime == ServiceLifetime.Singleton)
                .Should().Be(0);
    }

    [TestMethod]
    public void AddApiEndpointsFromAssembly_WithScopedPublic_ReturnsPublicServices()
    {
        // arrange
        IServiceCollection services = new ServiceCollection();
        var assembly = this.GetType().Assembly;

        // act
        services = services.AddApiEndpointsFromAssembly(assembly, includeInternalTypes: false);

        // assert
        services.Should().NotBeEmpty();
        services.Should().HaveCount(2);
    }

    [TestMethod]
    public void AddApiEndpointsFromAssembly_WithSingletonInternals_ReturnsServices()
    {
        // arrange
        IServiceCollection services = new ServiceCollection();
        var assembly = this.GetType().Assembly;

        // act
        services = services.AddApiEndpointsFromAssembly(assembly, ServiceLifetime.Singleton);

        // assert
        services.Should().NotBeEmpty();
        services.Should().HaveCount(3);
        services.Count((descriptor) => descriptor.Lifetime == ServiceLifetime.Scoped)
                .Should().Be(0);
        services.Count((descriptor) => descriptor.Lifetime == ServiceLifetime.Singleton)
                .Should().Be(3);
    }

    [TestMethod]
    public void MapApiEndpoints_WithMultipleEndpoints_MapsRoutes()
    {
        // arrange
        var builder = WebApplication.CreateBuilder();
        var assembly = this.GetType().Assembly;

        builder.Services.AddApiEndpointsFromAssembly(assembly);
        var app = builder.Build();

        // act
        app.MapApiEndpoints();

        // assert
        var routeBuilder = app as IEndpointRouteBuilder;
        routeBuilder.DataSources.Should().HaveCount(1);
        routeBuilder.DataSources.First().Endpoints.Should().HaveCount(3);
    }
}
