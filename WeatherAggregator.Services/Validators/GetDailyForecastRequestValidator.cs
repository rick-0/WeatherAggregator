using FluentValidation;
using WeatherAggregator.Services.Dto;

namespace WeatherAggregator.Services.Validators;

public class GetDailyForecastRequestValidator : AbstractValidator<GetDailyForecastRequest>
{
    public GetDailyForecastRequestValidator()
    {
        RuleFor(x => x.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
        RuleFor(x => x.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
        RuleFor(x => x.Days).GreaterThan(0);
    }
}
