using D20Tek.Minimal.Endpoints.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;

namespace D20Tek.Minimal.Endpoints.UnitTests.Exceptions;

[TestClass]
public class ExceptionHandlerRegistrationTests
{
    [TestMethod]
    public void UseExceptionHandler_RegistersHandler()
    {
        // arrange
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        // act
        app.UseExceptionHandler<EndpointExceptionHandler>();

        // assert
    }

    [TestMethod]
    public async Task RunExceptionHandlerImpl()
    {
        // arrange
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        var context = CreateMockContext();

        // act - assert
        await Assert.ThrowsAsync<NullReferenceException>([ExcludeFromCodeCoverage] () => 
            ExceptionHandlerRegistration.RunHandlerImpl<EndpointExceptionHandler>(context.Object));
    }

    private static Mock<HttpContext> CreateMockContext(string path = "/test")
    {
        var exceptionFeature = new ExceptionHandlerFeature
        {
            Endpoint = new Endpoint(null, null, "TestEndpoint"),
            Error = new InvalidOperationException(),
            Path = path
        };

        var mockFeatures = new Mock<IFeatureCollection>();
        mockFeatures.Setup(x => x.Get<IExceptionHandlerFeature>())
                    .Returns(exceptionFeature);

        var mockContext = new Mock<HttpContext>();
        mockContext.Setup(x => x.Features).Returns(mockFeatures.Object);

        var loggerFact = new LoggerFactory();
        var mockProvider = new Mock<IServiceProvider>();
        mockProvider.Setup(x => x.GetService(typeof(ILoggerFactory)))
                    .Returns(loggerFact);

        mockContext.Setup(x => x.RequestServices).Returns(mockProvider.Object);

        return mockContext;
    }
}
