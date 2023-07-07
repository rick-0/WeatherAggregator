using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.Services.ETL.Dto;

namespace WeatherAggregator.Services.ETL.Transformers;

public interface IApiTransformer
{
    Task<(WeatherProviderType WeatherProviderType, ICollection<DailyForecastHourDto> ForecastDto)> GetDailyWeatherForecast(decimal latitude, decimal longitude);
}
