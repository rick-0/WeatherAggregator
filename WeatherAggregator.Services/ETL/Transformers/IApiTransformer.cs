using WeatherAggregator.Configuration.Enums;

namespace WeatherAggregator.Services.ETL.Transformers;

public interface IApiTransformer<T>
{
    Task<(WeatherProviderType WeatherProviderType, T ForecastDto)> GetDailyWeatherForecast(decimal latitude, decimal longitude, int days);
}
