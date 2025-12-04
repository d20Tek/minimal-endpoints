namespace D20Tek.Minimal.Endpoints;

public sealed record HttpContextEnvelope<TBody>(HttpContext Context, ClaimsPrincipal User, TBody Body)
    : IRequest<IResult>
    where TBody : class;
