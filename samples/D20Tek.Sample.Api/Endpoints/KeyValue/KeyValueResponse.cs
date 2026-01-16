//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
namespace D20Tek.Sample.Api.Endpoints.KeyValue;

internal sealed record KeyValueResponse(string Key, string Value) : IRequest<IResult>;
