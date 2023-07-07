using AutoMapper;
using WeatherAggregator.Configuration.Enums;
using WeatherAggregator.DAL.Queries;
using WeatherAggregator.Services.ETL.Dto;

namespace WeatherAggregator.Services.ETL.Transformers
{
    public abstract class BaseApiTransformer<T> : IApiTransformer
    {
        protected readonly IMapper _mapper;
        protected readonly IWeatherQuery<T> _weatherQuery;

        public BaseApiTransformer(IMapper mapper,
            IWeatherQuery<T> weatherQuery)
        {
            _mapper = mapper;
            _weatherQuery = weatherQuery;
        }

        public virtual async Task<(WeatherProviderType WeatherProviderType, ICollection<DailyForecastHourDto> ForecastDto)> GetDailyWeatherForecast(decimal latitude, decimal longitude)
        {
            try
            {
                var result = await _weatherQuery.GetDailyWeatherForecast(latitude, longitude);

                return (GetWeatherProviderType(), _mapper.Map<ICollection<DailyForecastHourDto>>(result));
            }
            catch (Exception e) 
            {
                //todo log for later
                return (GetWeatherProviderType(), new List<DailyForecastHourDto>());
            }
        }

        protected abstract WeatherProviderType GetWeatherProviderType();
    }
}