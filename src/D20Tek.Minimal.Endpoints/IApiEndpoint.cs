namespace D20Tek.Minimal.Endpoints;

public interface IApiEndpoint
{
    public void MapRoute(IEndpointRouteBuilder routeBuilder);
}

public interface IApiEndpoint<TRequest> : IApiEndpoint where TRequest : IRequest<IResult>
{
    public Task<IResult> HandleAsync(TRequest request, CancellationToken cancellation);
}

public interface IApiEndpoint<TRequest, THandler> : IApiEndpoint where TRequest : IRequest<IResult>
{
    public Task<IResult> HandleAsync(TRequest request, THandler handler, CancellationToken cancellation);
}

public interface IAuthenticatedApiEndpoint<TRequest> : IApiEndpoint where TRequest : IRequest<IResult>
{
    public Task<IResult> HandleAsync(TRequest request, ClaimsPrincipal principal, CancellationToken cancellation);
}
