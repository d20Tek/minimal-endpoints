﻿//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics.CodeAnalysis;

namespace D20Tek.Minimal.Endpoints.UnitTests.Exceptions;

[TestClass]
public class EndpointExceptionHandlerTests
{
    [TestMethod]
    public void HandleException_ContextWithoutExceptionHandleFeature_Returns500Result()
    {
        // arrange
        var context = new DefaultHttpContext();
        var handler = new EndpointExceptionHandler();

        // act
        var result = handler.HandleException(context);

        // assert
        var problem = result.As<ProblemHttpResult>();
        problem.Should().NotBeNull();
        problem.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    [TestMethod]
    [DataRow(typeof(NotImplementedException), StatusCodes.Status501NotImplemented)]
    [DataRow(typeof(UnauthorizedAccessException), StatusCodes.Status401Unauthorized)]
    [DataRow(typeof(MethodAccessException), StatusCodes.Status405MethodNotAllowed)]
    [DataRow(typeof(FormatException), StatusCodes.Status400BadRequest)]
    [DataRow(typeof(ArgumentException), StatusCodes.Status400BadRequest)]
    [DataRow(typeof(HttpResponseException), StatusCodes.Status404NotFound)]
    public void HandleException_ContextWithException_ReturnsProblemDetailsResult(Type exceptionType, int statusCode)
    {
        // arrange
        var mockContext = CreateMockContext(exceptionType);
        var handler = new EndpointExceptionHandler();

        // act
        var result = handler.HandleException(mockContext.Object);

        // assert
        var problem = result.As<ProblemHttpResult>();
        problem.Should().NotBeNull();
        problem.StatusCode.Should().Be(statusCode);
    }

    private Mock<HttpContext> CreateMockContext(
        Type exceptionType,
        string path = "/test")
    {
        var exception = CreateExceptionFromType(exceptionType);
        var exceptionFeature = new ExceptionHandlerFeature
        {
            Endpoint = new Endpoint(null, null, "TestEndpoint"),
            Error = exception,
            Path = path
        };

        var mockFeatures = new Mock<IFeatureCollection>();
        mockFeatures.Setup(x => x.Get<IExceptionHandlerFeature>())
                    .Returns(exceptionFeature);

        var mockContext = new Mock<HttpContext>();
        mockContext.Setup(x => x.Features).Returns(mockFeatures.Object);

        return mockContext;
    }

    private Exception CreateExceptionFromType(Type exceptionType)
    {
        if (exceptionType == typeof(HttpResponseException))
        {
            return new HttpResponseException("test", StatusCodes.Status404NotFound);
        }

        var constructor = exceptionType.GetConstructor(new Type[] { });
        var ex = constructor!.Invoke(null).As<Exception>();
        return ResolveException(ex);

        [ExcludeFromCodeCoverage]
        static Exception ResolveException(Exception ex) => 
            ex ?? new Exception();
    }
}
