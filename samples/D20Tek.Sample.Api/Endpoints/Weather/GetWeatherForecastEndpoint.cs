//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints;

namespace D20Tek.Sample.Api.Endpoints.Weather;

internal class GetWeatherForecastEndpoint : IApiEndpoint
{
    private readonly string[] summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public void MapRoute(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/weatherforecast", Handle)
            .Produces<WeatherForecastResponse>(StatusCodes.Status200OK)
            .WithName("GetWeatherForecast")
            .WithTags("Weather Service")
            .WithOpenApi();
    }

    private IResult Handle(CancellationToken cancellation)
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecastResponse
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            )).ToArray();

        return Results.Ok(forecast);
    }
}
