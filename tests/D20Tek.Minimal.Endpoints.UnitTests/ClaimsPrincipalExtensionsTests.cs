namespace D20Tek.Minimal.Endpoints.UnitTests;

[TestClass]
public class ClaimsPrincipalExtensionsTests
{
    [TestMethod]
    public void FindUserId_WithValidPrincipal_ReturnsGuid()
    {
        // arrange
        var userId = Guid.NewGuid();
        var principal = ClaimsPrincipalFactory.CreateTestPrincipal(userId);

        // act
        var result = principal.FindUserId();

        // assert
        result.Should().Be(userId);
    }

    [TestMethod]
    public void FindUserId_WithEmptyPrincipal_ReturnsEmptyGuid()
    {
        // arrange
        var principal = new ClaimsPrincipal();

        // act
        var result = principal.FindUserId();

        // assert
        result.Should().Be(Guid.Empty);
    }

    [TestMethod]
    public void FindUserId_WithNonGuidPrincipalId_ReturnsEmptyGuid()
    {
        // arrange
        var userId = Guid.NewGuid();
        var principal = ClaimsPrincipalFactory.CreateTestPrincipal(id: "test-id");

        // act
        var result = principal.FindUserId();

        // assert
        result.Should().Be(Guid.Empty);
    }
}