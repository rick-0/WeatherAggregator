using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.DAL.Models;
using WeatherAggregator.DAL.Queries;
using WeatherAggregator.Services.Dto;
using WeatherAggregator.Services.ETL.Transformers;
using WeatherAggregator.Services.MappingProfile;
using Xunit;

namespace WeatherAggregator.Tests.Services.ETL.Transformers;

public class OWMTransformerTests
{
    [Theory]
    [ClassData(typeof(OWMTransformerTestData.ValidResponses))]
    private async void GetDailyWeatherForecast_WhenResponseIsValid_ReturnMatchesExpected(
        OWMResponse owmResponse,
        (WeatherProviderType WeatherProviderType, DailyForecastDto ForecastDto) expected)
    {
        // Arrange
        var sut = GetSut(owmResponse);

        // Act
        var actual = await sut.OWMTransformer.GetDailyWeatherForecast(1, 1, 1);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    private async void GetDailyWeatherForecast_WhenQueryThrows_ExceptionCaughtAndLogged()
    {
        // Arrange
        var sut = GetSut(new OWMResponse());

        sut.Query.GetDailyWeatherForecast(Arg.Any<decimal>(), Arg.Any<decimal>(), Arg.Any<int>())
            .Throws(new InvalidCastException("Oops"));

        // Act
        var actual = await sut.OWMTransformer.GetDailyWeatherForecast(1, 1, 1);

        // Assert
        actual.Should().BeEquivalentTo((WeatherProviderType.OWM, new DailyForecastDto()));
        sut.Logger.ReceivedWithAnyArgs(1).LogError("");
    }

    private (OWMTransformer OWMTransformer,
        ILogger<OWMTransformer> Logger,
        IWeatherQuery<OWMResponse> Query)
        GetSut(OWMResponse owmResponse)
    {
        var logger = Substitute.For<ILogger<OWMTransformer>>();

        var config = new MapperConfiguration(c =>
        {
            c.AddProfile<ServiceMappingProfile>();
        });
        var mapper = config.CreateMapper();

        var query = Substitute.For<IWeatherQuery<OWMResponse>>();
        query.GetDailyWeatherForecast(Arg.Any<decimal>(), Arg.Any<decimal>(), Arg.Any<int>())
            .Returns(owmResponse);

        var transformer = new OWMTransformer(
            logger,
            mapper,
            query);

        return (transformer, logger, query);
    }
}