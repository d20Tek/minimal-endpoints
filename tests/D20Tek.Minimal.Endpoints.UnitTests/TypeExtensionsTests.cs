namespace D20Tek.Minimal.Endpoints.UnitTests;

[TestClass]
public class TypeExtensionsTests
{
    [TestMethod]
    public void ThrowIfNotInterface_WithInterface_Returns()
    {
        // arrange

        // act
        typeof(IApiEndpoint).ThrowIfNotInterface();
    }

    [TestMethod]
    public void ThrowIfNotInterface_WithClass_ThrowsException()
    {
        // arrange

        // act - assert
        Assert.Throws<InvalidOperationException>([ExcludeFromCodeCoverage] () =>
            typeof(CompositeEndpoint).ThrowIfNotInterface());
    }
}
