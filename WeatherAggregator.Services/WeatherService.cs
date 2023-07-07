using WeatherAggregator.Services.ETL;
using WeatherAggregator.Services.ETL.Dto;

namespace WeatherAggregator.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherAggregator<DailyForecastHourDto> _dailyForecastWeatherAggregator;

    public WeatherService(IWeatherAggregator<DailyForecastHourDto> dailyForecastWeatherAggregator)
    {
        _dailyForecastWeatherAggregator = dailyForecastWeatherAggregator;
    }

    public async Task<ICollection<DailyForecastHourDto>> GetWeatherForecast(decimal latitude, decimal longitude)
    {
        var result = await _dailyForecastWeatherAggregator.GetAggregatedWeather(latitude, longitude);
        return result;
    }
}