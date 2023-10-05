//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints.UnitTests.Helpers;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace D20Tek.Minimal.Endpoints.UnitTests;

[TestClass]
public class HttpContextEnvelopeTests
{
    [ExcludeFromCodeCoverage]
    public record Payload(string Id = "", string Name = "");

    [TestMethod]
    public void HttpContextEnvelope_Setter()
    {
        // arrange
        var context = new DefaultHttpContext();
        var user = ClaimsPrincipalFactory.CreateTestPrincipal();
        var body = new Payload("test-user-id", "Tester McTest");

        var request = new HttpContextEnvelope<Payload>(
            new DefaultHttpContext(),
            new ClaimsPrincipal(),
            new Payload());

        // act
        request = request with
        {
            Context = context,
            User = user,
            Body = body
        };

        // assert
        request.Should().NotBeNull();
        request.Context.Should().Be(context);
        request.User.Should().Be(user);
        request.Body.Should().Be(body);
    }
}
