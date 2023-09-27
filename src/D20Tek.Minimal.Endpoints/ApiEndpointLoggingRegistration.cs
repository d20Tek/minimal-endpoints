//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace D20Tek.Minimal.Endpoints;

public static class ApiEndpointLoggingRegistration
{
    private static ApiEndpointLoggingFilter _endpointLogger = new();

    public static IApplicationBuilder UseApiEndpointLogging(this IApplicationBuilder app)
    {
        app.Use(InvokeHandler);

        return app;
    }

    internal static async Task InvokeHandler(HttpContext context, Func<Task> next)
    {
        await _endpointLogger.InvokeAsync(context, next);
    }
}
