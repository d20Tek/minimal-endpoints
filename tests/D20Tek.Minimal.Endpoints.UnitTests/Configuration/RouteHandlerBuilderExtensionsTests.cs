using D20Tek.Minimal.Endpoints.Configuration;
using Microsoft.AspNetCore.Builder;

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
            Tags: ["Weather Service"],
            Summary: "This is a test endpoint.",
            Description: "Long-winded description of the test endpoint.",
            Produces: [ Config.Produces<TestResponse>(StatusCodes.Status200OK) ]);

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
            Tags: ["Weather Service"],
            Produces:
            [
                Config.Produces<TestResponse>(StatusCodes.Status200OK),
                Config.Produces(StatusCodes.Status204NoContent),
                Config.ProducesProblem(StatusCodes.Status404NotFound),
                Config.ProducesValidationProblem()
            ]);

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
            Tags: ["Weather Service"],
            Accepts:
            [
                Config.Accepts<TestRequest>("application/json", true),
                Config.Accepts(typeof(TestRequest), "text/plain")
            ]);

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
            Tags: ["Weather Service"],
            RequiresAuthorization: true,
            AuthorizationPolicies: ["TestAuthPolicy", "AdminPolicy"],
            Produces: [ Config.Produces<TestResponse>(StatusCodes.Status200OK) ]);

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
            Tags: ["Weather Service"],
            RequiresAuthorization: true,
            Produces: [ Config.Produces<TestResponse>(StatusCodes.Status200OK) ]);

        var builder = CreateRouteBuilder();

        // act
        var result = builder.WithConfiguration(config);

        // assert
        result.Should().NotBeNull();
    }

    private static RouteHandlerBuilder CreateRouteBuilder()
    {
        var app = WebApplication.CreateBuilder().Build();
        return app.MapGet("/", [ExcludeFromCodeCoverage] () => { });
    }

    public class TestRequest { }

    public class TestResponse { }
}
