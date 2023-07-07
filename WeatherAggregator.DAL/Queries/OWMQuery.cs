using RestSharp.Authenticators;
using RestSharp;
using WeatherAggregator.Configuration;
using WeatherAggregator.DAL.Models;
using System.Xml.Serialization;

namespace WeatherAggregator.DAL.Queries
{
    public class OWMQuery : IWeatherQuery<OWMResponse>
    {
        private readonly IAppConfiguration _appConfiguration;

        public OWMQuery(IAppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public async Task<OWMResponse> GetDailyWeatherForecast(decimal latitude, decimal longitude)
        {
            //api.openweathermap.org/data/2.5/forecast?lat=44.34&lon=10.99&appid={API key}
            var options = new RestClientOptions("https://api.openweathermap.org/data/2.5/forecast");
            using var client = new RestClient(options);

            var request = new RestRequest()
                .AddParameter("appid", "572b8f10746ec15d6e8c62f8aff0619a")
                .AddParameter("lat", latitude)
                .AddParameter("lon", longitude)
                .AddParameter("units", "metric")
                .AddParameter("mode", "xml");

            var response = await client.GetAsync<OWMResponse>(request) ?? throw new InvalidOperationException(); // todo make new exception?;
            return response;
        }
    }
}