using WeatherAggregator.Services.Dto;
using Xunit;

namespace WeatherAggregator.Tests.Services.Validators;

public class GetDailyForecastRequestValidatorTestData
{
    public class ValidRequests : TheoryData<GetDailyForecastRequest>
    {
        public ValidRequests()
        {
            Add(new()
            {
                Days = 1,
                Latitude = 0,
                Longitude = 0
            });
            Add(new()
            {
                Days = 2,
                Latitude = 90,
                Longitude = 180
            });
            Add(new()
            {
                Days = 3,
                Latitude = -90,
                Longitude = -180
            });
        }
    }

    public class InvalidRequests : TheoryData<GetDailyForecastRequest>
    {
        public InvalidRequests()
        {
            Add(new()
            {
                Days = 0,
                Latitude = 0,
                Longitude = 0
            });
            Add(new()
            {
                Days = -1,
                Latitude = 0,
                Longitude = 0
            });
            Add(new()
            {
                Days = 1,
                Latitude = 90.1m,
                Longitude = 180
            });
            Add(new()
            {
                Days = 1,
                Latitude = 90,
                Longitude = 180.1m
            }); 
            Add(new()
            {
                Days = 1,
                Latitude = -90.1m,
                Longitude = -180
            });
            Add(new()
            {
                Days = 1,
                Latitude = -90,
                Longitude = -180.1m
            });
        }
    }
}
