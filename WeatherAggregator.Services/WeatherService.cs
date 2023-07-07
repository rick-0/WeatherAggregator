using FluentValidation;
using WeatherAggregator.Services.Dto;
using WeatherAggregator.Services.ETL;

namespace WeatherAggregator.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherAggregator<DailyForecastDto> _dailyForecastWeatherAggregator;
    private readonly IValidator<GetDailyForecastRequest> _getDailyForecastRequestValidator;

    public WeatherService(
        IWeatherAggregator<DailyForecastDto> dailyForecastWeatherAggregator,
        IValidator<GetDailyForecastRequest> getDailyForecastRequestValidator)
    {
        _dailyForecastWeatherAggregator = dailyForecastWeatherAggregator;
        _getDailyForecastRequestValidator = getDailyForecastRequestValidator;
    }

    public async Task<DailyForecastDto> GetDailyWeatherForecast(GetDailyForecastRequest getDailyForecastRequest)
    {
        var valid = await _getDailyForecastRequestValidator.ValidateAsync(getDailyForecastRequest);
        if (!valid.IsValid)
        {
            throw new InvalidOperationException(string.Join(", ", valid.Errors));
        }

        var result = await _dailyForecastWeatherAggregator.GetAggregatedWeather(getDailyForecastRequest.Latitude, getDailyForecastRequest.Longitude, getDailyForecastRequest.Days);
        return result;
    }
}