using Microsoft.AspNetCore.Mvc;
using WeatherAggregator.Services;

namespace WeatherAggregator.Controllers
{
    public class WeatherController
    {
        public static void RegisterController(WebApplication app)
        {
            app.MapGet("/weather/forecast/daily", async (IWeatherService weatherService, decimal? latitude, decimal? longitude) =>
            {
                return await weatherService.GetWeatherForecast(latitude ?? 54.6013401301571m, longitude ?? -5.92436446018419m);
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

            //app.MapGet("/{id}", (int id,
            //         int page,
            //         [FromHeader(Name = "X-CUSTOM-HEADER")] string customHeader,
            //         Service service) => { });

            //app.MapGet("/tags", (int[] q) =>
            //          $"tag1: {q[0]} , tag2: {q[1]}, tag3: {q[2]}");
        }
    }
}
