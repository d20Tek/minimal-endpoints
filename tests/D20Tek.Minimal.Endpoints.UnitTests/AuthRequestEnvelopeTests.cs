//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints.UnitTests.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace D20Tek.Minimal.Endpoints.UnitTests;

[TestClass]
public class AuthRequestEnvelopeTests
{
    [ExcludeFromCodeCoverage]
    public record Payload(string Id = "", string Name = "");

    [TestMethod]
    public void AuthRequestEnvelope_Setter()
    {
        // arrange
        var user = ClaimsPrincipalFactory.CreateTestPrincipal();
        var body = new Payload("test-user-id", "Tester McTest");
        var request = new AuthRequestEnvelope<Payload>(
            new ClaimsPrincipal(), new Payload());

        // act
        request = request with { User = user, Body = body };

        // assert
        request.Should().NotBeNull();
        request.User.Should().Be(user);
        request.Body.Should().Be(body);
    }
}
