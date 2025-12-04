namespace D20Tek.Minimal.Endpoints.Exceptions;

public class HttpResponseException : Exception
{
    private const string _defaultTitle = "Internal Server Error";
    private const string _defaultType = "/errors/unknown-error";

    public HttpResponseException(
        string detail,
        int? statusCode = null,
        string? title = null,
        string? type = null)
        : base(detail)
    {
        StatusCode = statusCode ?? StatusCodes.Status500InternalServerError;
        Title = title ?? _defaultTitle;
        Type = type ?? _defaultType;
    }

    public HttpResponseException(
        Exception fromException,
        int? statusCode = null,
        string? title = null,
        string? type = null)
        : base(fromException.Message, fromException)
    {
        StatusCode = statusCode ?? StatusCodes.Status500InternalServerError;
        Title = title ?? _defaultTitle;
        Type = type ?? _defaultType;
    }

    public int StatusCode { get; }

    public string Title { get; }

    public string Type { get; }
}
