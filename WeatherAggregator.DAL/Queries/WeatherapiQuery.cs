using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using WeatherAggregator.Configuration;
using WeatherAggregator.Configuration.Configs;
using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.Configuration.Extensions;
using WeatherAggregator.DAL.Models;

namespace WeatherAggregator.DAL.Queries
{
    public class WeatherapiQuery : IWeatherQuery<WeatherapiResponse>
    {
        private readonly ApiEndpointConfigurations _apiEndpointConfig;

        public WeatherapiQuery(IAppConfiguration appConfiguration)
        {
            var providerName = WeatherProviderType.Weatherapi.ToDescriptionString();
            _apiEndpointConfig = appConfiguration.ApiConfiguration.ApiProviderConfigs[providerName];
        }

        public async Task<WeatherapiResponse> GetDailyWeatherForecast(decimal latitude, decimal longitude, int days)
        {
            //http://api.weatherapi.com/v1/forecast.json?key={key}&q=London&days=2&aqi=no&alerts=no
            //todo caching => LazyCache/redis/etc, base class to handle caching
            var options = new RestClientOptions(_apiEndpointConfig.DailyForecastEndpoint);
            using var client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());

            var request = new RestRequest()
                .AddParameter("key", _apiEndpointConfig.ApiKey)
                .AddParameter("q", $"{latitude}, {longitude}")
                .AddParameter("days", days)
                .AddParameter("aqi", "no")
                .AddParameter("alerts", "no");

            var response = await client.GetAsync<WeatherapiResponse>(request) ?? throw new InvalidOperationException($"No parseable content from {WeatherProviderType.Weatherapi} api");
            return response;
        }
    }
}