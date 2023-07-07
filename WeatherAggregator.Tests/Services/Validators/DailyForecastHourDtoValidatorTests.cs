using FluentAssertions;
using WeatherAggregator.Services.Dto;
using WeatherAggregator.Services.Validators;
using Xunit;

namespace WeatherAggregator.Tests.Services.Validators;

public class DailyForecastHourDtoValidatorTests
{
    [Theory]
    [ClassData(typeof(DailyForecastHourDtoValidatorTestData.ValidRequests))]
    private void Validate_WhenInputIsValid_ResultValid(DailyForecastHourDto input)
    {
        // Arrange
        var validator = new DailyForecastHourDtoValidator();

        // Act
        var result = validator.Validate(input);

        // Assert
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [ClassData(typeof(DailyForecastHourDtoValidatorTestData.InvalidRequests))]
    private void Validate_WhenInputIsInvalid_ResultInvalid(DailyForecastHourDto input)
    {
        // Arrange
        var validator = new DailyForecastHourDtoValidator();

        // Act
        var result = validator.Validate(input);

        // Assert
        result.IsValid.Should().Be(false);
    }
}
