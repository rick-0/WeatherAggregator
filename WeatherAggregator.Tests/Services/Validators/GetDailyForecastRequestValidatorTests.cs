using FluentAssertions;
using WeatherAggregator.Services.Dto;
using WeatherAggregator.Services.Validators;
using Xunit;

namespace WeatherAggregator.Tests.Services.Validators;

public class GetDailyForecastRequestValidatorTests
{
    [Theory]
    [ClassData(typeof(GetDailyForecastRequestValidatorTestData.ValidRequests))]
    private void Validate_WhenRequestIsValid(GetDailyForecastRequest request)
    {
        // Arrange
        var validator = new GetDailyForecastRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [ClassData(typeof(GetDailyForecastRequestValidatorTestData.InvalidRequests))]
    private void Validate_WhenRequestIsInvalid(GetDailyForecastRequest request)
    {
        // Arrange
        var validator = new GetDailyForecastRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().Be(false);
    }
}
