using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.DAL.Models;
using WeatherAggregator.Services.Dto;
using Xunit;

namespace WeatherAggregator.Tests.Services.ETL.Transformers;

public class WeatherapiTransformerTestData
{
    public class ValidResponses : TheoryData<WeatherapiResponse, (WeatherProviderType WeatherProviderType, DailyForecastDto ForecastDto)>
    {
        private readonly DateTime NineAm = new DateTime(2023, 1, 1, 9, 0, 0);
        private readonly DateTime Noon = new DateTime(2023, 1, 1, 9, 0, 0);

        public ValidResponses()
        {
            Add(
                new WeatherapiResponse
                {
                    Location = new WeatherapiLocation { Latitude = 1, Longitude = 2 },
                    Forecast = new WeatherapiForecast
                    {
                        ForecastDay = new List<WeatherapiForecastDay>
                        {
                            new()
                            {
                                Hour = new List<WeatherapiHour>
                                {
                                    new()
                                    {
                                        Time = NineAm,
                                        TempC = 2,
                                        TempF = 35.6m,
                                        WindKph = 54,
                                        WindDegrees = 100,
                                        ChanceOfRain = 70
                                    }
                                }
                            }
                        }
                    }
                },
                (WeatherProviderType.Weatherapi, new DailyForecastDto
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
                        }
                    }
                }));
        }
    }
}
