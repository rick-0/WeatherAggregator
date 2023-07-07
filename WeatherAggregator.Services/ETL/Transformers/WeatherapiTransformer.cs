using AutoMapper;
using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.DAL.Models;
using WeatherAggregator.DAL.Queries;
using WeatherAggregator.Services.ETL.Dto;

namespace WeatherAggregator.Services.ETL.Transformers
{
    public class WeatherapiTransformer : BaseApiTransformer<WeatherapiResponse>
    {
        public WeatherapiTransformer(IMapper mapper, IWeatherQuery<WeatherapiResponse> weatherQuery) : base(mapper, weatherQuery)
        {
        }

        public override async Task<(WeatherProviderType WeatherProviderType, ICollection<DailyForecastHourDto> ForecastDto)> GetDailyWeatherForecast(decimal latitude, decimal longitude)
        {
            try
            {
                var results = await _weatherQuery.GetDailyWeatherForecast(latitude, longitude);

                var mappedResults = results.Forecast.ForecastDay.SelectMany(x => _mapper.Map<ICollection<DailyForecastHourDto>>(x.Hour)).ToList();

                return (GetWeatherProviderType(), mappedResults);
            }
            catch (Exception e)
            {
                //todo log for later
                return (GetWeatherProviderType(), new List<DailyForecastHourDto>());
            }
        }

        protected override WeatherProviderType GetWeatherProviderType()
        {
            return WeatherProviderType.Weatherapi;
        }
    }
}