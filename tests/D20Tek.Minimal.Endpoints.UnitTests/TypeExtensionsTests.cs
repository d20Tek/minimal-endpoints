//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints.UnitTests.MockEndpoints;
using System.Diagnostics.CodeAnalysis;

namespace D20Tek.Minimal.Endpoints.UnitTests;

[TestClass]
public class TypeExtensionsTests
{
    [TestMethod]
    public void ThrowIfNotInterface_WithInterface_Returns()
    {
        // arrange

        // act
        typeof(IApiEndpoint).ThrowIfNotInterface();
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ThrowIfNotInterface_WithClass_ThrowsException()
    {
        // arrange

        // act
        typeof(CompositeEndpoint).ThrowIfNotInterface();
    }
}
