namespace D20Tek.Minimal.Endpoints.UnitTests;

[TestClass]
public class HttpContextRequestTests
{
    [TestMethod]
    public void HttpContextRequest_Setter()
    {
        // arrange
        var context = new DefaultHttpContext();
        var user = ClaimsPrincipalFactory.CreateTestPrincipal();
        var request = new HttpContextRequest(new DefaultHttpContext(), new ClaimsPrincipal());

        // act
        request = request with { Context = context, User = user };

        // assert
        request.Should().NotBeNull();
        request.Context.Should().Be(context);
        request.User.Should().Be(user);
    }
}
