//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Http;

namespace D20Tek.Minimal.Endpoints;

public interface IRequest<out TResult>
    where TResult : IResult
{
}
