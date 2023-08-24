//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
namespace D20Tek.Minimal.Endpoints;

public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource source);
}
