//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace D20Tek.Minimal.Endpoints.Exceptions;

public class EndpointExceptionHandler
{
    public IResult HandleException(HttpContext context)
    {
        var exceptionHandlerFeature =
            context.Features.Get<IExceptionHandlerFeature>();

        var error = exceptionHandlerFeature?.Error ?? new InvalidOperationException();
        var path = exceptionHandlerFeature?.Path ?? string.Empty;

        return HandleException(error, path);
    }

    protected virtual IResult HandleException(Exception exception, string instancePath)
    {
        return exception switch
        {
            NotImplementedException ex =>
                Results.Problem(ex.Message, instancePath, StatusCodes.Status501NotImplemented),
            UnauthorizedAccessException ex =>
                Results.Problem(ex.Message, instancePath, StatusCodes.Status401Unauthorized),
            MethodAccessException ex =>
                Results.Problem(ex.Message, instancePath, StatusCodes.Status405MethodNotAllowed),
            FormatException ex =>
                Results.Problem(ex.Message, instancePath, StatusCodes.Status400BadRequest),
            ArgumentException ex =>
                Results.Problem(ex.Message, instancePath, StatusCodes.Status400BadRequest),
            HttpResponseException ex =>
                Results.Problem(ex.Message, instancePath, ex.StatusCode, ex.Title, ex.Type),
            _ => Results.Problem(exception.Message, instancePath, StatusCodes.Status500InternalServerError),
        };
    }
}
