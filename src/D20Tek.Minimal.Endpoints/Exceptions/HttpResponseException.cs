//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Http;

namespace D20Tek.Minimal.Endpoints.Exceptions;

public class HttpResponseException : Exception
{
    public HttpResponseException(
        string detail,
        int? statusCode = null,
        string? title = null,
        string? type = null)
        : base(detail)
    {
        StatusCode = statusCode ?? StatusCodes.Status500InternalServerError;
        Title = title ?? "Internal Server Error";
        Type = type ?? "/errors/unknown-error";
    }

    public HttpResponseException(
        Exception fromException,
        int? statusCode = null,
        string? title = null,
        string? type = null)
        : base(fromException.Message, fromException)
    {
        StatusCode = statusCode ?? StatusCodes.Status500InternalServerError;
        Title = title ?? "Internal Server Error";
        Type = type ?? "/errors/unknown-error";
    }

    public int StatusCode { get; }

    public string Title { get; }

    public string Type { get; }
}
