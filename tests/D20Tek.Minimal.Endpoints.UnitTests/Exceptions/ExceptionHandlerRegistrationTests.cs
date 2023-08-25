//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace D20Tek.Minimal.Endpoints.UnitTests.Exceptions;

[TestClass]
public class ExceptionHandlerRegistrationTests
{
    [TestMethod]
    public void UseExceptionHandler_RegistersHandler()
    {
        // arrange
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        // act
        app.UseExceptionHandler<EndpointExceptionHandler>();

        // assert
    }
}
