using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.Services.Dto;
using Xunit;

namespace WeatherAggregator.Tests.Services.ETL.Transformers;

public class DailyForecastWeatherAggregatorTestData
{
    public class ValidResponses : TheoryData<int, ICollection<(WeatherProviderType, DailyForecastDto)>, ICollection<WeatherProviderType>, DailyForecastDto>
    {
        public ValidResponses()
        {
            // Just 1 provider returned so the aggregator should only return that response unmodified
            Add(
                1,
                new List<(WeatherProviderType, DailyForecastDto)>
                {
                    (WeatherProviderType.Weatherapi,
                    new DailyForecastDto
                    {
                        Latitude = 1,
                        Longitude = 2,
                        HourlyForecast = CreateDailyForecastHourDto(24)
                    })
                },
                new List<WeatherProviderType>
                {
                    WeatherProviderType.Weatherapi
                },
                new DailyForecastDto
                {
                    Latitude = 1,
                    Longitude = 2,
                    HourlyForecast = CreateDailyForecastHourDto(24)
                });
            // 2 providers so aggregator should take the results based on provider priority, expect that Weatherapi takes precedence
            Add(
                1,
                new List<(WeatherProviderType, DailyForecastDto)>
                {
                    (WeatherProviderType.Weatherapi,
                    new DailyForecastDto
                    {
                        Latitude = 1,
                        Longitude = 2,
                        HourlyForecast = CreateDailyForecastHourDto(24)
                    }),
                    (WeatherProviderType.OWM,
                    new DailyForecastDto
                    {
                        Latitude = 44,
                        Longitude = 55,
                        HourlyForecast = CreateDailyForecastHourDto2(24)
                    })
                },
                new List<WeatherProviderType>
                {
                    WeatherProviderType.Weatherapi, WeatherProviderType.OWM
                },
                new DailyForecastDto
                {
                    Latitude = 1,
                    Longitude = 2,
                    HourlyForecast = CreateDailyForecastHourDto(24)
                });
            // 2 providers so aggregator should take the results based on provider priority, expect that OWM takes precedence
            Add(
                1,
                new List<(WeatherProviderType, DailyForecastDto)>
                {
                    (WeatherProviderType.Weatherapi,
                    new DailyForecastDto
                    {
                        Latitude = 1,
                        Longitude = 2,
                        HourlyForecast = CreateDailyForecastHourDto(24)
                    }),
                    (WeatherProviderType.OWM,
                    new DailyForecastDto
                    {
                        Latitude = 44,
                        Longitude = 55,
                        HourlyForecast = CreateDailyForecastHourDto2(24)
                    })
                },
                new List<WeatherProviderType>
                {
                    WeatherProviderType.OWM, WeatherProviderType.Weatherapi
                },
                new DailyForecastDto
                {
                    Latitude = 44,
                    Longitude = 55,
                    HourlyForecast = CreateDailyForecastHourDto2(24)
                });
        }

        private ICollection<DailyForecastHourDto> CreateDailyForecastHourDto(int count)
        {
            var utcNow = DateTime.UtcNow;
            var dt = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, 0, 0);

            var list = new List<DailyForecastHourDto>();
            for (var i = 0; i < count; ++i)
            {
                var dto = new DailyForecastHourDto
                {
                    TimeUtc = dt.AddHours(i),
                    PrecipitationChance = 1,
                    TemperatureC = 1,
                    TemperatureF = 35.6m,
                    WindDirectionDeg = 0,
                    WindSpeedKph = 12
                };
                list.Add(dto);
            }

            return list;
        }

        private ICollection<DailyForecastHourDto> CreateDailyForecastHourDto2(int count)
        {
            var utcNow = DateTime.UtcNow;
            var dt = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, 0, 0);

            var list = new List<DailyForecastHourDto>();
            for (var i = 0; i < count; ++i)
            {
                var dto = new DailyForecastHourDto
                {
                    TimeUtc = dt.AddHours(i),
                    PrecipitationChance = 56,
                    TemperatureC = 55,
                    TemperatureF = 66,
                    WindDirectionDeg = 123,
                    WindSpeedKph = 1000 // :(
                };
                list.Add(dto);
            }

            return list;
        }
    }
}
