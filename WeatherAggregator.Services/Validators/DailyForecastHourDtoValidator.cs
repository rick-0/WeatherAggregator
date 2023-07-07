using FluentValidation;
using WeatherAggregator.Services.Dto;

namespace WeatherAggregator.Services.Validators;

public class DailyForecastHourDtoValidator : AbstractValidator<DailyForecastHourDto>
{
    public DailyForecastHourDtoValidator()
    {
        RuleFor(x => x.TemperatureC).NotNull();
        RuleFor(x => x.TemperatureF).NotNull();
        RuleFor(x => x.WindSpeedKph).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.WindDirectionDeg).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.PrecipitationChance).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.TimeUtc).NotEqual(new DateTime());
    }
}
