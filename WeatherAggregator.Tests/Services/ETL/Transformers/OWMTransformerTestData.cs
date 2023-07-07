using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.DAL.Models;
using WeatherAggregator.Services.Dto;
using Xunit;

namespace WeatherAggregator.Tests.Services.ETL.Transformers;

public class OWMTransformerTestData
{
    public class ValidResponses : TheoryData<OWMResponse, (WeatherProviderType WeatherProviderType, DailyForecastDto ForecastDto)>
    {
        private readonly DateTime NineAm = new DateTime(2023, 1, 1, 9, 0, 0);
        private readonly DateTime Noon = new DateTime(2023, 1, 1, 9, 0, 0);

        public ValidResponses()
        {
            Add(
                new OWMResponse
                {
                    Location = new OWMLocation { Location = new OWMCoords { Latitude = 1, Longitude = 2 } },
                    Forecast = new OWMForecast
                    {
                        Time = new List<OWMTime>
                    {
                        new OWMTime
                        {
                            From = NineAm, To = Noon,
                            Precipitation = new OWMPrecipitation { Probability = 0.7m },
                            Temperature = new OWMTemperature { Value = 2 },
                            WindDirection = new OWMWindDirection { Deg = 100 },
                            WindSpeed = new OWMWindSpeed { MetresPerSecond = 15 }
                        }
                    }
                    }
                },
                (WeatherProviderType.OWM, new DailyForecastDto
                {
                    Latitude = 1,
                    Longitude = 2,
                    HourlyForecast = new List<DailyForecastHourDto>
                    {
                        new()
                        {
                            TimeUtc = NineAm,
                            PrecipitationChance = 70,
                            TemperatureC = 2,
                            TemperatureF = 35.6m,
                            WindDirectionDeg = 100,
                            WindSpeedKph = 54
                        },
                        new()
                        {
                            TimeUtc = NineAm.AddHours(1),
                            PrecipitationChance = 70,
                            TemperatureC = 2,
                            TemperatureF = 35.6m,
                            WindDirectionDeg = 100,
                            WindSpeedKph = 54
                        },
                        new()
                        {
                            TimeUtc = NineAm.AddHours(2),
                            PrecipitationChance = 70,
                            TemperatureC = 2,
                            TemperatureF = 35.6m,
                            WindDirectionDeg = 100,
                            WindSpeedKph = 54
                        }
                    }
                }));
        }
    }
}
