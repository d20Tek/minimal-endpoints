namespace D20Tek.Minimal.Endpoints.UnitTests.Helpers;

internal static class ClaimsPrincipalFactory
{
    public static ClaimsPrincipal CreateTestPrincipal(
        string givenName = "Tester",
        string familyName = "McTest",
        string email = "mctest@test.com") =>
        CreateTestPrincipal(Guid.NewGuid(), givenName, familyName, email);

    public static ClaimsPrincipal CreateTestPrincipal(
        Guid id,
        string givenName = "Tester",
        string familyName = "McTest",
        string email = "mctest@test.com")
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, id.ToString()),
            new(ClaimTypes.GivenName, givenName),
            new(ClaimTypes.Surname, familyName),
            new(ClaimTypes.Name, givenName),
            new(ClaimTypes.Email, email),
            new("jti", Guid.NewGuid().ToString()),
            new("scope", "d20Tek.HabitTracker.ReadWrite"),
            new("iss", "d20Tek.AuthService"),
            new("aud", "d20Tek.HabitTracker")
        };

        return new ClaimsPrincipal(new ClaimsIdentity(claims));
    }
}
