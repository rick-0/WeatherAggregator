using WeatherAggregator.Configuration.Configs;

namespace WeatherAggregator.Configuration;

public interface IAppConfiguration
{
    WeatherApiConfiguration ApiConfiguration { get; set; }
}