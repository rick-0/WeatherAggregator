using WeatherAggregator.Configuration;
using WeatherAggregator.Services.ETL.Dto;
using WeatherAggregator.Services.ETL.Transformers;

namespace WeatherAggregator.Services.ETL
{
    public class DailyForecastWeatherAggregator : IWeatherAggregator<DailyForecastHourDto>
    {
        private readonly IAppConfiguration _appConfiguration;
        private readonly IEnumerable<IApiTransformer> _transformers;

        public DailyForecastWeatherAggregator(IAppConfiguration appConfiguration,
            IEnumerable<IApiTransformer> transformers)
        {
            _appConfiguration = appConfiguration;
            _transformers = transformers;
        }

        public async Task<ICollection<DailyForecastHourDto>> GetAggregatedWeather(decimal latitude, decimal longitude)
        {
            var tasks = _transformers.Select(x => x.GetDailyWeatherForecast(latitude, longitude));
            var results = (await Task.WhenAll(tasks)).ToDictionary(x => x.WeatherProviderType, y => y.ForecastDto);

            var primaryForecastDto = new List<DailyForecastHourDto>();
            var priorities = _appConfiguration.ApiConfiguration.WeatherProvidersPriority;
            // merge all forecasts into one forecast based on provider priority list
            foreach (var providerType in priorities)
            {
                if (!results.TryGetValue(providerType, out var secondaryForecastDto)) { continue; }
                MergeForecastDto(primaryForecastDto.Any() ? primaryForecastDto : secondaryForecastDto.ToList(), secondaryForecastDto.ToList());
            }

            return primaryForecastDto;
        }

        private void MergeForecastDto(List<DailyForecastHourDto> dtos1, List<DailyForecastHourDto> dtos2)
        {
            for (int i = 0; i < dtos1.Count; i++)
            {
                var dto1 = dtos1[i];
                var dto2 = dtos2[i];

                // todo see if automapper can do this? I think it can, but should it?
                dto1.Latitude = dto1.Latitude == 0 ? dto2.Latitude : dto1.Latitude;
                dto1.Longitude = dto1.Longitude == 0 ? dto2.Longitude : dto1.Longitude;
                dto1.TimeUtc = dto1.TimeUtc == default ? dto2.TimeUtc : dto1.TimeUtc;
                dto1.TemperatureC = dto1.TemperatureC == 0 ? dto2.TemperatureC : dto1.TemperatureC;
                dto1.TemperatureF = dto1.TemperatureF == 0 ? dto2.TemperatureF : dto1.TemperatureF;
                dto1.WindSpeedKph = dto1.WindSpeedKph == 0 ? dto2.WindSpeedKph : dto1.WindSpeedKph;
                dto1.WindDirectionDeg = dto1.WindDirectionDeg == 0 ? dto2.WindDirectionDeg : dto1.WindDirectionDeg;
                dto1.PrecipitationChance = dto1.PrecipitationChance == 0 ? dto2.PrecipitationChance : dto1.PrecipitationChance;
            }
        }
    }
}