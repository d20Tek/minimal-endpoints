using D20Tek.Minimal.Endpoints.Configuration;

namespace D20Tek.Minimal.Endpoints.UnitTests.Configuration;

[TestClass]
public class RecordSetterTests
{
    [TestMethod]
    public void EndpointAcceptsConfig_Setter()
    {
        // arrange
        var type = typeof(string);
        var contentType = "text/plain";
        var isOptional = true;
        var others = new string[] { "test1", "test2" };

        var config = new EndpointAcceptsConfig(typeof(string), "");

        // act
        var result = config with
        {
            RequestType = type,
            ContentType = contentType,
            IsOptional = isOptional,
            AdditionalContentTypes = others
        };

        // assert
        result.Should().NotBeNull();
        result.RequestType.Should().Be(type);
        result.ContentType.Should().Be(contentType);
        result.IsOptional.Should().BeTrue();
        result.AdditionalContentTypes.Should().BeEquivalentTo(others);
    }

    [TestMethod]
    public void EndpointProducesConfig_Setter()
    {
        // arrange
        var statusCode = 200;
        var responseType = typeof(string);
        var contentType = "text/plain";
        var others = new string[] { "test1", "test2" };

        var config = new EndpointProducesConfig(500);

        // act
        var result = config with
        {
            StatusCode = statusCode,
            ResponseType = responseType,
            ContentType = contentType,
            AdditionalContentTypes = others
        };

        // assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(statusCode);
        result.ResponseType.Should().Be(responseType);
        result.ContentType.Should().Be(contentType);
        result.AdditionalContentTypes.Should().BeEquivalentTo(others);
    }

    [TestMethod]
    public void ApiEndpointConfig_Setter()
    {
        // arrange
        var route = "/test";
        var name = "TestName";
        var display = "Test Display Name";
        var tags = new[] { "TestCategory" };
        var summary = "test summary";
        var description = "long test description";
        var requiresAuth = true;
        var policies = new[] { "TestPolicy" };
        var produces = new[] { Config.Produces(StatusCodes.Status201Created) };
        var accepts = new[] { Config.Accepts(typeof(string), "text/plain") };

        var config = new ApiEndpointConfig("/", "");

        // act
        var result = config with
        {
            RoutePattern = route,
            EndpointName = name,
            DisplayName = display,
            Tags = tags,
            Summary = summary,
            Description = description,
            RequiresAuthorization = requiresAuth,
            AuthorizationPolicies = policies,
            Produces = produces,
            Accepts = accepts
        };

        // assert
        result.Should().NotBeNull();
        result.RoutePattern.Should().Be(route);
        result.EndpointName.Should().Be(name);
        result.DisplayName.Should().Be(display);
        result.Tags.Should().BeEquivalentTo(tags);
        result.Summary.Should().Be(summary);
        result.Description.Should().Be(description);
        result.RequiresAuthorization.Should().BeTrue();
        result.AuthorizationPolicies.Should().BeEquivalentTo(policies);
        result.Produces.Should().BeEquivalentTo(produces);
        result.Accepts.Should().BeEquivalentTo(accepts);
    }
}
