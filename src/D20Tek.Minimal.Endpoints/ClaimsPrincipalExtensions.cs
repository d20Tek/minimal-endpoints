namespace D20Tek.Minimal.Endpoints;

public static class ClaimsPrincipalExtensions
{
    public static Guid FindUserId(this ClaimsPrincipal principal)
    {
        ArgumentNullException.ThrowIfNull(principal);

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Guid.Empty;

        return Guid.TryParse(userId, out Guid result) ? result : Guid.Empty;
    }
}
