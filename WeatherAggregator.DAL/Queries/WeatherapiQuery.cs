using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using WeatherAggregator.Configuration;
using WeatherAggregator.DAL.Models;

namespace WeatherAggregator.DAL.Queries
{
    public class WeatherapiQuery : IWeatherQuery<WeatherapiResponse>
    {
        private readonly IAppConfiguration _appConfiguration;

        public WeatherapiQuery(IAppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public async Task<WeatherapiResponse> GetDailyWeatherForecast(decimal latitude, decimal longitude)
        {
            //http://api.weatherapi.com/v1/forecast.json?key={key}&q=London&days=2&aqi=no&alerts=no
            var options = new RestClientOptions("http://api.weatherapi.com/v1/forecast.json");
            using var client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());

            var request = new RestRequest()
                .AddParameter("key", "283b5e61c97146259bd143433230607")
                .AddParameter("q", $"{latitude}, {longitude}")
                .AddParameter("days", "5")
                .AddParameter("aqi", "no")
                .AddParameter("alerts", "no");

            var response = await client.GetAsync<WeatherapiResponse>(request) ?? throw new InvalidOperationException(); // todo make new exception?
            return response;
        }
    }
}