//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints.Exceptions;
using Microsoft.AspNetCore.Http;

namespace D20Tek.Minimal.Endpoints.UnitTests.Asserts;

public static class HttpsResponseExceptionAssertions
{
    public static void ShouldBe(
        this HttpResponseException ex,
        InvalidOperationException? fromException,
        string details,
        int code = StatusCodes.Status500InternalServerError,
        string title = "Internal Server Error",
        string type = "/errors/unknown-error")
    {
        ex.Should().NotBeNull();
        ex.InnerException.Should().Be(fromException);
        ex.Message.Should().Be(details);
        ex.StatusCode.Should().Be(code);
        ex.Title.Should().Be(title);
        ex.Type.Should().Be(type);
    }
}
