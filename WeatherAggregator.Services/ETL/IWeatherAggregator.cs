using WeatherAggregator.Services.ETL.Dto;

namespace WeatherAggregator.Services.ETL
{
    public interface IWeatherAggregator<T>
    {
        Task<ICollection<T>> GetAggregatedWeather(decimal latitude, decimal longitude);
    }
}