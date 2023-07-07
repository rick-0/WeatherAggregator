using AutoMapper;
using Microsoft.Extensions.Logging;
using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.DAL.Models;
using WeatherAggregator.DAL.Queries;
using WeatherAggregator.Services.Dto;

namespace WeatherAggregator.Services.ETL.Transformers
{
    public class WeatherapiTransformer : IApiTransformer<DailyForecastDto>
    {
        private readonly ILogger<WeatherapiTransformer> _logger;
        protected readonly IMapper _mapper;
        protected readonly IWeatherQuery<WeatherapiResponse> _weatherQuery;

        public WeatherapiTransformer(
            ILogger<WeatherapiTransformer> logger, 
            IMapper mapper,
            IWeatherQuery<WeatherapiResponse> weatherQuery)
        {
            _logger = logger;
            _mapper = mapper;
            _weatherQuery = weatherQuery;
        }

        public async Task<(WeatherProviderType WeatherProviderType, DailyForecastDto ForecastDto)>
            GetDailyWeatherForecast(decimal latitude, decimal longitude, int days)
        {
            try
            {
                var results = await _weatherQuery.GetDailyWeatherForecast(latitude, longitude, days);

                var mappedResults = results.Forecast.ForecastDay.SelectMany(x => _mapper.Map<ICollection<DailyForecastHourDto>>(x.Hour)).ToList();

                var dailyForecast = new DailyForecastDto
                {
                    Latitude = results.Location.Latitude,
                    Longitude = results.Location.Longitude,
                    HourlyForecast = mappedResults
                };

                return (WeatherProviderType.Weatherapi, dailyForecast);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{nameof(WeatherapiTransformer)} could not complete transformation of results");
                return (WeatherProviderType.Weatherapi, new DailyForecastDto());
            }
        }
    }
}