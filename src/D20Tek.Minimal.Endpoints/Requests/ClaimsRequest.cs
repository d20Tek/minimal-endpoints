namespace D20Tek.Minimal.Endpoints;

public sealed record ClaimsRequest(ClaimsPrincipal User) : IRequest<IResult>;
