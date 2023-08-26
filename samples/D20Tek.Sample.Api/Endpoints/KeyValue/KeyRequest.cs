//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints;

namespace D20Tek.Sample.Api.Endpoints.KeyValue;

internal sealed record KeyRequest(string Key) : IRequest<IResult>;
