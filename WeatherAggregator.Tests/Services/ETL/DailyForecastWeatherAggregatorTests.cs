using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using WeatherAggregator.Configuration;
using WeatherAggregator.Configuration.Configs;
using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.Services.Dto;
using WeatherAggregator.Services.ETL;
using WeatherAggregator.Services.ETL.Transformers;
using WeatherAggregator.Tests.Services.ETL.Transformers;
using Xunit;

namespace WeatherAggregator.Tests.Services.ETL;

public class DailyForecastWeatherAggregatorTests
{
    [Theory]
    [ClassData(typeof(DailyForecastWeatherAggregatorTestData.ValidResponses))]
    private async void GetAggregatedWeather_WhenValidResponses_ReturnMatchesExpected(
        int days,
        ICollection<(WeatherProviderType, DailyForecastDto)> dailyForecasts,
        ICollection<WeatherProviderType> weatherProviderTypePriorities,
        DailyForecastDto expected)
    {
        // Arrange
        var sut = GetSut(dailyForecasts, weatherProviderTypePriorities);

        // Act
        var actual = await sut.GetAggregatedWeather(1, 1, days);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    private DailyForecastWeatherAggregator
    GetSut(ICollection<(WeatherProviderType, DailyForecastDto)> dailyForecasts,
        ICollection<WeatherProviderType> weatherProviderTypePriorities)
    {
        var appConfiguration = Substitute.For<IAppConfiguration>();
        appConfiguration.ApiConfiguration.Returns(
            new WeatherApiConfiguration
            {
                WeatherProvidersPriority = weatherProviderTypePriorities
            });

        var transformers = new List<IApiTransformer<DailyForecastDto>>();
        foreach (var dailyForecast in dailyForecasts)
        {
            var transformer = Substitute.For<IApiTransformer<DailyForecastDto>>();
            transformer.GetDailyWeatherForecast(Arg.Any<decimal>(), Arg.Any<decimal>(), Arg.Any<int>())
                .Returns(dailyForecast);

            transformers.Add(transformer);
        }

        var validator = Substitute.For<IValidator<DailyForecastHourDto>>();
        validator.Validate(Arg.Any<DailyForecastHourDto>()).Returns(new ValidationResult());

        var dailyForecastWeatherAggregator = new DailyForecastWeatherAggregator(
            appConfiguration,
            transformers,
            validator);

        return dailyForecastWeatherAggregator;
    }
}
