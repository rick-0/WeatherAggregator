using Microsoft.Extensions.Configuration;
using WeatherAggregator.Configuration.Configs;

namespace WeatherAggregator.Configuration;

public class AppConfiguration : IAppConfiguration
{
    public ApiConfiguration ApiConfiguration { get; set; }

    public AppConfiguration(IConfiguration configuration)
    {
        ApiConfiguration = configuration.GetSection("Api").Get<ApiConfiguration>() ?? throw new InvalidOperationException($"{nameof(ApiConfiguration)} could not be found in appsettings");
    }
}