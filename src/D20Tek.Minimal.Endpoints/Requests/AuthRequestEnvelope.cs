//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace D20Tek.Minimal.Endpoints;

public sealed record AuthRequestEnvelope<TBody>(
    ClaimsPrincipal User,
    TBody Body) : IRequest<IResult>
        where TBody : class;
