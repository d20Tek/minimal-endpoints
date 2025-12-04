namespace D20Tek.Minimal.Endpoints.UnitTests;

[TestClass]
public class EmptyRequestTests
{
    [TestMethod]
    public void CreateEmptyRequest()
    {
        // arrange

        // act
        var request = new EmptyRequest();

        // assert
        request.Should().NotBeNull();
    }
}
