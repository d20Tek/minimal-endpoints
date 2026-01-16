//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
namespace D20Tek.Sample.Api.Endpoints.KeyValue;

internal sealed record KeyValueRequest(string Key, string Value)
    : IRequest<IResult>
{
    public KeyValueResponse ToResponse()
    {
        return new KeyValueResponse(Key, Value);
    }
}
