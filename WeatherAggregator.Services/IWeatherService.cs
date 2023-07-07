using WeatherAggregator.Services.Dto;

namespace WeatherAggregator.Services;

public interface IWeatherService
{
    public Task<DailyForecastDto> GetDailyWeatherForecast(GetDailyForecastRequest getDailyForecastRequest);
}
