namespace D20Tek.Minimal.Endpoints.Exceptions;

public static class ExceptionHandlerRegistration
{
    public static IApplicationBuilder UseExceptionHandler<THandler>(this IApplicationBuilder app)
        where THandler : EndpointExceptionHandler, new() =>
        app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(RunHandlerImpl<THandler>));

    [ExcludeFromCodeCoverage]
    internal static async Task RunHandlerImpl<THandler>(HttpContext context)
        where THandler : EndpointExceptionHandler, new()
    {
        var handler = new THandler();
        await handler.HandleException(context)
                     .ExecuteAsync(context);
    }
}
