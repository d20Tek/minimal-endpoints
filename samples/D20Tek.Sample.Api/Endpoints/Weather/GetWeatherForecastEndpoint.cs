using D20Tek.Minimal.Endpoints.Configuration;

namespace D20Tek.Sample.Api.Endpoints.Weather;

internal sealed class GetWeatherForecastEndpoint : IApiEndpoint
{
    private readonly string[] summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    private static readonly ApiEndpointConfig _config = new(
        "/weatherforecast",
        "GetWeatherForecast",
        Tags: ["Weather Service"],
        Produces:
        [
            Config.Produces<WeatherForecastResponse>(StatusCodes.Status200OK)
        ]);

    public void MapRoute(IEndpointRouteBuilder routeBuilder) => 
        routeBuilder.MapGet(_config.RoutePattern, Handle)
                    .WithConfiguration(_config)
                    .WithOpenApi();

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
