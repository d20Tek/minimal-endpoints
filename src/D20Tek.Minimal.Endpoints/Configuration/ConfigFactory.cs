//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D20Tek.Minimal.Endpoints.Configuration;

public static class Config
{
    public static EndpointAcceptsConfig Accepts<T>(
        string contentType,
        bool isOptional = false,
        string[]? additionalContentTypes = null)
    {
        return new(typeof(T), contentType, isOptional, additionalContentTypes);
    }

    public static EndpointAcceptsConfig Accepts(
        Type requestType,
        string contentType,
        bool isOptional = false,
        string[]? additionalContentTypes = null)
    {
        return new(requestType, contentType, isOptional, additionalContentTypes);
    }

    public static EndpointProducesConfig Produces(
        int statusCode,
        string? contentType = null,
        string[]? additionalContentTypes = null)
    {
        return new(statusCode, null, contentType, additionalContentTypes);
    }

    public static EndpointProducesConfig Produces<T>(
        int statusCode,
        string? contentType = null,
        string[]? additionalContentTypes = null)
    {
        return new(statusCode, typeof(T), contentType, additionalContentTypes);
    }

    public static EndpointProducesConfig ProducesProblem(
        int statusCode,
        string? contentType = null)
    {
        return new(statusCode, typeof(ProblemDetails), contentType);
    }

    public static EndpointProducesConfig ProducesValidationProblem(
        int statusCode = 400,
        string? contentType = null)
    {
        return new(statusCode, typeof(HttpValidationProblemDetails), contentType);
    }
}
