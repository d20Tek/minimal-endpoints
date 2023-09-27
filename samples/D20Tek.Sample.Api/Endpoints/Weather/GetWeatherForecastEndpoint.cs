//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using D20Tek.Minimal.Endpoints;
using D20Tek.Minimal.Endpoints.Configuration;

namespace D20Tek.Sample.Api.Endpoints.Weather;

internal class GetWeatherForecastEndpoint : IApiEndpoint
{
    private readonly string[] summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private static ApiEndpointConfig _config = new ApiEndpointConfig(
        "/weatherforecast",
        "GetWeatherForecast",
        Tags: new string[] { "Weather Service" },
        Produces: new[]
        {
            Config.Produces<WeatherForecastResponse>(StatusCodes.Status200OK)
        });

    public void MapRoute(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(_config.RoutePattern, Handle)
            .WithConfiguration(_config)
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
