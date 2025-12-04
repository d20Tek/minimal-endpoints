namespace D20Tek.Minimal.Endpoints.UnitTests;

[TestClass]
public class ClaimsRequestTests
{
    [TestMethod]
    public void ClaimsRequest_Setter()
    {
        // arrange
        var user = ClaimsPrincipalFactory.CreateTestPrincipal();
        var request = new ClaimsRequest(new ClaimsPrincipal());

        // act
        request = request with { User = user };

        // assert
        request.Should().NotBeNull();
        request.User.Should().Be(user);
    }
}
