//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;

namespace D20Tek.Minimal.Endpoints.Exceptions;

public static class ExceptionHandlerRegistration
{
    public static IApplicationBuilder UseExceptionHandler<THandler>(this IApplicationBuilder app)
        where THandler : EndpointExceptionHandler, new()
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
            exceptionHandlerApp.Run(async (context) =>
            {
                var handler = new THandler();
                await handler.HandleException(context)
                             .ExecuteAsync(context);
            }));

        return app;
    }
}
