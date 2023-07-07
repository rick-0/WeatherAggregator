namespace WeatherAggregator.Services.ETL
{
    public interface IWeatherAggregator<T>
    {
        Task<T> GetAggregatedWeather(decimal latitude, decimal longitude, int days);
    }
}