//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace D20Tek.Minimal.Endpoints.UnitTests.Configuration;

[TestClass]
public class RouteHandlerBuilderExtensionsTests
{
    [TestMethod]
    public void WithConfiguration_SimpleMetadata_ProducesRoute()
    {
        // arrange
        var config = new ApiEndpointConfig(
            "/",
            "GetTest",
            "Get Test",
            Tags: new string[] { "Weather Service" },
            Summary: "This is a test endpoint.",
            Description: "Long-winded description of the test endpoint.",
            Produces: new[]
            {
                Config.Produces<TestResponse>(StatusCodes.Status200OK)
            });

        var builder = CreateRouteBuilder();

        // act
        var result = builder.WithConfiguration(config);

        // assert
        result.Should().NotBeNull();
    }

    [TestMethod]
    public void WithConfiguration_ComplexProducesMetadata_ProducesRoute()
    {
        // arrange
        var config = new ApiEndpointConfig(
            "/",
            "GetTest",
            "Get Test",
            Tags: new string[] { "Weather Service" },
            Produces: new[]
            {
                Config.Produces<TestResponse>(StatusCodes.Status200OK),
                Config.Produces(StatusCodes.Status204NoContent),
                Config.ProducesProblem(StatusCodes.Status404NotFound),
                Config.ProducesValidationProblem()
            });

        var builder = CreateRouteBuilder();

        // act
        var result = builder.WithConfiguration(config);

        // assert
        result.Should().NotBeNull();
    }

    [TestMethod]
    public void WithConfiguration_ComplexAcceptsMetadata_ProducesRoute()
    {
        // arrange
        var config = new ApiEndpointConfig(
            "/",
            "GetTest",
            Tags: new string[] { "Weather Service" },
            Accepts: new[]
            {
                Config.Accepts<TestRequest>("application/json", true),
                Config.Accepts(typeof(TestRequest), "text/plain")
            });

        var builder = CreateRouteBuilder();

        // act
        var result = builder.WithConfiguration(config);

        // assert
        result.Should().NotBeNull();
    }

    [TestMethod]
    public void WithConfiguration_AuthenticationMetadata_ProducesRoute()
    {
        // arrange
        var config = new ApiEndpointConfig(
            "/",
            "GetTest",
            Tags: new string[] { "Weather Service" },
            RequiresAuthorization: true,
            AuthorizationPolicies: new[] { "TestAuthPolicy", "AdminPolicy" },
            Produces: new[]
            {
                Config.Produces<TestResponse>(StatusCodes.Status200OK)
            });

        var builder = CreateRouteBuilder();

        // act
        var result = builder.WithConfiguration(config);

        // assert
        result.Should().NotBeNull();
    }

    [TestMethod]
    public void WithConfiguration_AuthenticationMetadataDefaultPolicy_ProducesRoute()
    {
        // arrange
        var config = new ApiEndpointConfig(
            "/",
            "GetTest",
            Tags: new string[] { "Weather Service" },
            RequiresAuthorization: true,
            Produces: new[]
            {
                Config.Produces<TestResponse>(StatusCodes.Status200OK)
            });

        var builder = CreateRouteBuilder();

        // act
        var result = builder.WithConfiguration(config);

        // assert
        result.Should().NotBeNull();
    }

    private RouteHandlerBuilder CreateRouteBuilder()
    {
        var appBuilder = WebApplication.CreateBuilder();
        var app = appBuilder.Build();

        var routeBuilder = app.MapGet("/", [ExcludeFromCodeCoverage] () => { });
        return routeBuilder;
    }

    public class TestRequest { }

    public class TestResponse { }
}
