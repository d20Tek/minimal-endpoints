//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using System.Security.Claims;

namespace D20Tek.Minimal.Endpoints.UnitTests.Helpers;

internal static class ClaimsPrincipalFactory
{
    public static ClaimsPrincipal CreateTestPrincipal(
        string givenName = "Tester",
        string familyName = "McTest",
        string email = "mctest@test.com")
    {
        return CreateTestPrincipal(Guid.NewGuid(), givenName, familyName, email);
    }

    public static ClaimsPrincipal CreateTestPrincipal(
        Guid id,
        string givenName = "Tester",
        string familyName = "McTest",
        string email = "mctest@test.com")
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.GivenName, givenName),
            new Claim(ClaimTypes.Surname, familyName),
            new Claim(ClaimTypes.Name, givenName),
            new Claim(ClaimTypes.Email, email),
            new Claim("jti", Guid.NewGuid().ToString()),
            new Claim("scope", "d20Tek.HabitTracker.ReadWrite"),
            new Claim("iss", "d20Tek.AuthService"),
            new Claim("aud", "d20Tek.HabitTracker")
        };
        var identity = new ClaimsIdentity(claims);

        var principal = new ClaimsPrincipal(identity);
        return principal;
    }
}
