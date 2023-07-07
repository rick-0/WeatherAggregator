using WeatherAggregator.Configuration.Enums;

namespace WeatherAggregator.Configuration.Configs;

public class WeatherApiConfiguration
{
    public IDictionary<string, ApiEndpointConfigurations> ApiProviderConfigs { get; set; } = new Dictionary<string, ApiEndpointConfigurations>();
    public ICollection<WeatherProviderType> WeatherProvidersPriority { get; set; } = new List<WeatherProviderType>();
}

public class ApiEndpointConfigurations
{
    public string ApiKey { get; set; } = string.Empty;
    public string DailyForecastEndpoint { get; set; } = string.Empty;
}