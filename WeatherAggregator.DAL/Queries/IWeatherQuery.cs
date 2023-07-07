namespace WeatherAggregator.DAL.Queries;

public interface IWeatherQuery<T>
{
    Task<T> GetDailyWeatherForecast(decimal latitude, decimal longitude, int days);
}
