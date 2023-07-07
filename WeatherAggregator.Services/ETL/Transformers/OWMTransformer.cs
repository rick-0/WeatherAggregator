using AutoMapper;
using Microsoft.Extensions.Logging;
using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.DAL.Models;
using WeatherAggregator.DAL.Queries;
using WeatherAggregator.Services.Dto;

namespace WeatherAggregator.Services.ETL.Transformers
{
    public class OWMTransformer : IApiTransformer<DailyForecastDto>
    {
        private readonly ILogger<OWMTransformer> _logger;
        protected readonly IMapper _mapper;
        protected readonly IWeatherQuery<OWMResponse> _weatherQuery;

        public OWMTransformer(
            ILogger<OWMTransformer> logger,
            IMapper mapper,
            IWeatherQuery<OWMResponse> weatherQuery)
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

                var mappedResults = results.Forecast.Time.Select(_mapper.Map<DailyForecastHourDto>).ToList();

                // OWM api returns data in 3 hour intervals, but we need them in hourly
                // So duplicate data twice to make it 'hourly', this is fine as it's a backup api
                var newResults = new List<DailyForecastHourDto>();
                foreach (var result in mappedResults)
                {
                    newResults.AddRange(new []
                    {
                        result with { TimeUtc = result.TimeUtc }, 
                        result with { TimeUtc = result.TimeUtc.AddHours(1) }, 
                        result with { TimeUtc = result.TimeUtc.AddHours(2) }
                    });
                }

                var dailyForecast = new DailyForecastDto
                {
                    Latitude = results.Location.Location.Latitude,
                    Longitude = results.Location.Location.Longitude,
                    HourlyForecast = newResults
                };

                return (WeatherProviderType.OWM, dailyForecast);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{nameof(OWMTransformer)} could not complete transformation of results");
                return (WeatherProviderType.OWM, new DailyForecastDto());
            }
        }
    }
}