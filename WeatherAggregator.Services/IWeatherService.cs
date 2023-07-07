using WeatherAggregator.Services.ETL.Dto;

namespace WeatherAggregator.Services;

public interface IWeatherService
{
    public Task<ICollection<DailyForecastHourDto>> GetWeatherForecast(decimal latitude, decimal longitude);
}
