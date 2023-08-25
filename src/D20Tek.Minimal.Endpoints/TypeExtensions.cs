//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
namespace D20Tek.Minimal.Endpoints;

public static class TypeExtensions
{
    public static void ThrowIfNotInterface(this Type type)
    {
        if (type.IsInterface is false)
        {
            throw new InvalidOperationException(
                "Cannot GetAssemblyTypes with generic type that isn't an interface.");
        }
    }
}
