//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints.Exceptions;
using D20Tek.Minimal.Endpoints.UnitTests.Asserts;
using Microsoft.AspNetCore.Http;

namespace D20Tek.Minimal.Endpoints.UnitTests.Exceptions;

[TestClass]
public class HttpResponseExceptionTests
{
    [TestMethod]
    public void Exception_WithFullDetails()
    {
        // arrange
        string details = "details";
        int code = StatusCodes.Status409Conflict;
        string title = "Conflict";
        string type = "conflict-type";

        // act
        var ex = new HttpResponseException(details, code, title, type);

        // assert
        ex.ShouldBe(null, details, code, title, type);
    }

    [TestMethod]
    public void Exception_WithDefaults()
    {
        // arrange
        string details = "details";

        // act
        var ex = new HttpResponseException(details);

        // assert
        ex.ShouldBe(null, details);
    }

    [TestMethod]
    public void InnerException_WithFullDetails()
    {
        // arrange
        var fromException = new InvalidOperationException("test");
        int code = StatusCodes.Status409Conflict;
        string title = "Conflict";
        string type = "conflict-type";

        // act
        var ex = new HttpResponseException(fromException, code, title, type);

        // assert
        ex.ShouldBe(fromException, "test", code, title, type);
    }

    [TestMethod]
    public void InnerException_WithDefaults()
    {
        // arrange
        var fromException = new InvalidOperationException("test");

        // act
        var ex = new HttpResponseException(fromException);

        // assert
        ex.ShouldBe(fromException, "test");
    }
}
