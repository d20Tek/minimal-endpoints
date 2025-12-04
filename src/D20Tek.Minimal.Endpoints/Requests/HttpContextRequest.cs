namespace D20Tek.Minimal.Endpoints;

public record HttpContextRequest(HttpContext Context, ClaimsPrincipal User) : IRequest<IResult>;
