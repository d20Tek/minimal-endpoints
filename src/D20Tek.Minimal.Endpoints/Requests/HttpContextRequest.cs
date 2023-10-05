//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace D20Tek.Minimal.Endpoints;

public record HttpContextRequest(
    HttpContext Context,
    ClaimsPrincipal User) : IRequest<IResult>;
