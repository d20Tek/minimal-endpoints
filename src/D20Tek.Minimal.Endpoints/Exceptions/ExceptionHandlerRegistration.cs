//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace D20Tek.Minimal.Endpoints.Exceptions;

public static class ExceptionHandlerRegistration
{
    public static IApplicationBuilder UseExceptionHandler<THandler>(this IApplicationBuilder app)
        where THandler : EndpointExceptionHandler, new()
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
            exceptionHandlerApp.Run(RunHandlerImpl<THandler>));

        return app;
    }

    [ExcludeFromCodeCoverage]
    internal static async Task RunHandlerImpl<THandler>(HttpContext context)
        where THandler : EndpointExceptionHandler, new()
    {
        var handler = new THandler();
        await handler.HandleException(context)
                     .ExecuteAsync(context);
    }
}
