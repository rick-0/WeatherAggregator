using WeatherAggregator.Configuration.Configs;

namespace WeatherAggregator.Configuration;

public interface IAppConfiguration
{
    ApiConfiguration ApiConfiguration { get; set; }
}