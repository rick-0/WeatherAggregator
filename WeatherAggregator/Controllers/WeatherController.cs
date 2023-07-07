using WeatherAggregator.Services;
using WeatherAggregator.Services.Dto;

namespace WeatherAggregator.Controllers
{
    public class WeatherController
    {
        public static void RegisterController(WebApplication app)
        {
            app.MapGet("/weather/forecast/daily", async (IWeatherService weatherService, decimal? latitude, decimal? longitude, int? days) =>
            {
                var request = new GetDailyForecastRequest
                {
                    Latitude = latitude ?? 54.6013401301571m,
                    Longitude = longitude ?? -5.92436446018419m,
                    Days = 5 // todo implement variable days
                };
                return await weatherService.GetDailyWeatherForecast(request);
            })
            .WithName("GetDailyWeatherForecast")
            .WithOpenApi(generatedOperation =>
            {
                var parameter = generatedOperation.Parameters[0];
                parameter.Description = "Return weather data for this Latitude";
                parameter = generatedOperation.Parameters[1];
                parameter.Description = "Return weather data for this Longitude";
                parameter = generatedOperation.Parameters[2];
                parameter.Description = "NOT IMPLEMENTED - Number of days forecast data to return";
                return generatedOperation;
            });
        }
    }
}
