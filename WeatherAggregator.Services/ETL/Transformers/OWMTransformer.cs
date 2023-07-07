using AutoMapper;
using System.Drawing;
using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.DAL.Models;
using WeatherAggregator.DAL.Queries;
using WeatherAggregator.Services.ETL.Dto;

namespace WeatherAggregator.Services.ETL.Transformers
{
    public class OWMTransformer : BaseApiTransformer<OWMResponse>
    {
        public OWMTransformer(IMapper mapper, IWeatherQuery<OWMResponse> weatherQuery) : base(mapper, weatherQuery)
        {
        }

        public override async Task<(WeatherProviderType WeatherProviderType, ICollection<DailyForecastHourDto> ForecastDto)> GetDailyWeatherForecast(decimal latitude, decimal longitude)
        {
            try
            {
                var results = await _weatherQuery.GetDailyWeatherForecast(latitude, longitude);

                var mappedResults = results.Forecast.Time.Select(_mapper.Map<DailyForecastHourDto>).ToList();

                // OWM api returns data in 3 hour intervals, but we need them in hourly
                // So duplicate data twice to make it 'hourly', this is fine as it's a backup api
                var newResults = new List<DailyForecastHourDto>();
                foreach (var result in mappedResults)
                {
                    newResults.AddRange(new []
                    {
                        result with { TimeUtc = result.TimeUtc }, 
                        result with { TimeUtc = result.TimeUtc?.AddHours(1) }, 
                        result with { TimeUtc = result.TimeUtc?.AddHours(2) }
                    });
                }

                return (GetWeatherProviderType(), newResults);
            }
            catch (Exception e)
            {
                //todo log for later
                return (GetWeatherProviderType(), new List<DailyForecastHourDto>());
            }
        }

        protected override WeatherProviderType GetWeatherProviderType()
        {
            return WeatherProviderType.OWM;
        }
    }
}