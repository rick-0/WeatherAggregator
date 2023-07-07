using RestSharp;
using WeatherAggregator.Configuration;
using WeatherAggregator.DAL.Models;
using WeatherAggregator.Configuration.Configs;
using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.Configuration.Extensions;

namespace WeatherAggregator.DAL.Queries
{
    public class OWMQuery : IWeatherQuery<OWMResponse>
    {
        private readonly ApiEndpointConfigurations _apiEndpointConfig;

        public OWMQuery(IAppConfiguration appConfiguration)
        {
            var providerName = WeatherProviderType.OWM.ToDescriptionString();
            _apiEndpointConfig = appConfiguration.ApiConfiguration.ApiProviderConfigs[providerName];
        }

        public async Task<OWMResponse> GetDailyWeatherForecast(decimal latitude, decimal longitude, int days)
        {
            //api.openweathermap.org/data/2.5/forecast?lat=44.34&lon=10.99&appid={API key}
            //todo caching => LazyCache/redis/etc, base class to handle caching
            var options = new RestClientOptions(_apiEndpointConfig.DailyForecastEndpoint);
            using var client = new RestClient(options);

            var request = new RestRequest()
                .AddParameter("appid", _apiEndpointConfig.ApiKey)
                .AddParameter("lat", latitude)
                .AddParameter("lon", longitude)
                .AddParameter("units", "metric")
                .AddParameter("mode", "xml");

            var response = await client.GetAsync<OWMResponse>(request) ?? throw new InvalidOperationException($"No parseable content from {WeatherProviderType.OWM} api");
            return response;
        }
    }
}