//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Http;

namespace D20Tek.Minimal.Endpoints;

public sealed record EmptyRequest() : IRequest<IResult>;
