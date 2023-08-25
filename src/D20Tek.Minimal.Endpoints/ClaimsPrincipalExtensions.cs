//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using System.Security.Claims;

namespace D20Tek.Minimal.Endpoints;

public static class ClaimsPrincipalExtensions
{
    public static Guid FindUserId(this ClaimsPrincipal principal)
    {
        ArgumentNullException.ThrowIfNull(principal);

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Guid.Empty;

        return Guid.Parse(userId);
    }
}
