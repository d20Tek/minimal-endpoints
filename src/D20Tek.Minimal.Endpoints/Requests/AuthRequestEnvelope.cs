namespace D20Tek.Minimal.Endpoints;

public sealed record AuthRequestEnvelope<TBody>(ClaimsPrincipal User, TBody Body) : IRequest<IResult>
    where TBody : class;
