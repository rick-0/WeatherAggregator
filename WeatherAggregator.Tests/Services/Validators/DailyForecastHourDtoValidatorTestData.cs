using WeatherAggregator.Services.Dto;
using Xunit;

namespace WeatherAggregator.Tests.Services.Validators;

public class DailyForecastHourDtoValidatorTestData
{
    public class ValidRequests : TheoryData<DailyForecastHourDto>
    {
        public ValidRequests()
        {
            Add(new()
            {
                PrecipitationChance = 0,
                TemperatureC = 0,
                TemperatureF = 0,
                WindSpeedKph = 0,
                WindDirectionDeg = 0,
                TimeUtc = DateTime.UtcNow
            });
            Add(new()
            {
                PrecipitationChance = 1,
                TemperatureC = -1,
                TemperatureF = -1,
                WindSpeedKph = 1,
                WindDirectionDeg = 1,
                TimeUtc = DateTime.UtcNow
            });
            Add(new()
            {
                PrecipitationChance = 1,
                TemperatureC = 1,
                TemperatureF = 1,
                WindSpeedKph = 1,
                WindDirectionDeg = 1,
                TimeUtc = DateTime.UtcNow
            });
        }
    }

    public class InvalidRequests : TheoryData<DailyForecastHourDto>
    {
        public InvalidRequests()
        {
            Add(new()
            {
            });
            Add(new()
            {
                PrecipitationChance = -1,
                TemperatureC = 1,
                TemperatureF = 1,
                WindSpeedKph = 1,
                WindDirectionDeg = 1,
                TimeUtc = DateTime.UtcNow
            });
            Add(new()
            {
                PrecipitationChance = 1,
                TemperatureC = 1,
                TemperatureF = 1,
                WindSpeedKph = -1,
                WindDirectionDeg = 1,
                TimeUtc = DateTime.UtcNow
            });
            Add(new()
            {
                PrecipitationChance = 1,
                TemperatureC = 1,
                TemperatureF = 1,
                WindSpeedKph = 1,
                WindDirectionDeg = -1,
                TimeUtc = DateTime.UtcNow
            });
            Add(new()
            {
                PrecipitationChance = 1,
                TemperatureC = -1,
                TemperatureF = 1,
                WindSpeedKph = 1,
                WindDirectionDeg = 1,
                TimeUtc = new DateTime()
            });
        }
    }
}
