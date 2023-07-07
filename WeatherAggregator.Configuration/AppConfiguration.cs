using Microsoft.Extensions.Configuration;
using WeatherAggregator.Configuration.Configs;

namespace WeatherAggregator.Configuration;

public class AppConfiguration : IAppConfiguration
{
    public WeatherApiConfiguration ApiConfiguration { get; set; }

    public AppConfiguration(IConfiguration configuration)
    {
        ApiConfiguration = configuration.GetSection("Api").Get<WeatherApiConfiguration>()
            ?? throw new InvalidOperationException($"{nameof(ApiConfiguration)} could not be found in appsettings");
    }
}