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

public class WeatherapiTransformerTests
{
    [Theory]
    [ClassData(typeof(WeatherapiTransformerTestData.ValidResponses))]
    private async void GetDailyWeatherForecast_WhenResponseIsValid_ReturnMatchesExpected(
    WeatherapiResponse response,
    (WeatherProviderType WeatherProviderType, DailyForecastDto ForecastDto) expected)
    {
        // Arrange
        var sut = GetSut(response);

        // Act
        var actual = await sut.OWMTransformer.GetDailyWeatherForecast(1, 1, 1);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    private async void GetDailyWeatherForecast_WhenQueryThrows_ExceptionCaughtAndLogged()
    {
        // Arrange
        var sut = GetSut(new WeatherapiResponse());

        sut.Query.GetDailyWeatherForecast(Arg.Any<decimal>(), Arg.Any<decimal>(), Arg.Any<int>())
            .Throws(new InvalidCastException("Oops"));

        // Act
        var actual = await sut.OWMTransformer.GetDailyWeatherForecast(1, 1, 1);

        // Assert
        actual.Should().BeEquivalentTo((WeatherProviderType.Weatherapi, new DailyForecastDto()));
        sut.Logger.ReceivedWithAnyArgs(1).LogError("");
    }

    private (WeatherapiTransformer OWMTransformer,
        ILogger<WeatherapiTransformer> Logger,
        IWeatherQuery<WeatherapiResponse> Query)
        GetSut(WeatherapiResponse response)
    {
        var logger = Substitute.For<ILogger<WeatherapiTransformer>>();

        var config = new MapperConfiguration(c =>
        {
            c.AddProfile<ServiceMappingProfile>();
        });
        var mapper = config.CreateMapper();

        var query = Substitute.For<IWeatherQuery<WeatherapiResponse>>();
        query.GetDailyWeatherForecast(Arg.Any<decimal>(), Arg.Any<decimal>(), Arg.Any<int>())
            .Returns(response);

        var transformer = new WeatherapiTransformer(
            logger,
            mapper,
            query);

        return (transformer, logger, query);
    }
}