//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using System.Text;

namespace D20Tek.Minimal.Endpoints.UnitTests;

[TestClass]
public class ApiEndpointLoggingFilterTests
{
    [TestMethod]
    public async Task InvokeAsync_WithRequestBody_LogsData()
    {
        // arrange
        var context = CreateHttpContext("test body");
        var filter = new ApiEndpointLoggingFilter();
        var nextCalled = false;

        // act
        var result = await filter.InvokeAsync(context, () => 
        {
            context.Response.ContentType = "text/plain";
            byte[] contentBytes = Encoding.UTF8.GetBytes("Ok");
            context.Response.Body.Write(contentBytes);
            context.Response.StatusCode = 200;

            nextCalled = true;
            return Task.CompletedTask;
        });

        // assert
        result.Should().NotBeNull();
        var value = (bool)result!;
        value.Should().BeTrue();
        nextCalled.Should().BeTrue();
    }

    [TestMethod]
    public void RegisterLoggingFilter()
    {
        // arrange
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        // act
        app.UseApiEndpointLogging();

        // assert
    }

    [TestMethod]
    public async Task InvokeHandler()
    {
        // arrange
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        var context = CreateHttpContext("");
        var filter = new ApiEndpointLoggingFilter();
        var nextCalled = false;

        // act
        await ApiEndpointLoggingRegistration.InvokeHandler(
            context, () =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            });

        // assert
        nextCalled.Should().BeTrue();
    }

    public HttpContext CreateHttpContext(string bodyText)
    {
        var context = new DefaultHttpContext();

        var requestFeature = new HttpRequestFeature();

        requestFeature.Protocol = "https";
        requestFeature.Method = "POST";
        requestFeature.Scheme = "https";
        requestFeature.PathBase = new PathString("/");
        requestFeature.Path = new PathString("/example");
        requestFeature.Headers["HeaderName"] = "HeaderValue";

        if (string.IsNullOrEmpty(bodyText) is false)
        {
            byte[] contentBytes = Encoding.UTF8.GetBytes(bodyText);
            var stream = new MemoryStream(contentBytes);
            stream.Position = 0;

            requestFeature.Body = stream;
        }

        // set the request feature in the HttpContext
        context.Features.Set<IHttpRequestFeature>(requestFeature);

        var mockLogger = new Mock<ILogger<ApiEndpointLoggingFilter>>();
        var mockRequestServices = new Mock<IServiceProvider>();
        mockRequestServices.Setup(x => x.GetService(typeof(ILogger<ApiEndpointLoggingFilter>)))
                           .Returns(mockLogger.Object);

        context.RequestServices = mockRequestServices.Object;

        return context;
    }
}
