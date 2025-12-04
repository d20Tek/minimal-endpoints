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

    [TestMethod]
    public void ChangeSetters()
    {
        // arrange
        var request = new EmptyRequest();

        // act
        var result = request with { };

        // assert
        result.Should().NotBeNull();
    }
}
