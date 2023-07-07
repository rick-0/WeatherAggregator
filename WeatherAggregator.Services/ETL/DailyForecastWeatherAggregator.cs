using FluentValidation;
using WeatherAggregator.Configuration;
using WeatherAggregator.Services.Dto;
using WeatherAggregator.Services.ETL.Transformers;

namespace WeatherAggregator.Services.ETL
{
    public class DailyForecastWeatherAggregator : IWeatherAggregator<DailyForecastDto>
    {
        private readonly IAppConfiguration _appConfiguration;
        private readonly IEnumerable<IApiTransformer<DailyForecastDto>> _transformers;
        private readonly IValidator<DailyForecastHourDto> _dailyForecastHourDtoValidator;

        public DailyForecastWeatherAggregator(
            IAppConfiguration appConfiguration,
            IEnumerable<IApiTransformer<DailyForecastDto>> transformers,
            IValidator<DailyForecastHourDto> dailyForecastHourDtoValidator)
        {
            _appConfiguration = appConfiguration;
            _transformers = transformers;
            _dailyForecastHourDtoValidator = dailyForecastHourDtoValidator;
        }

        public async Task<DailyForecastDto> GetAggregatedWeather(decimal latitude, decimal longitude, int days)
        {
            var tasks = _transformers.Select(x => x.GetDailyWeatherForecast(latitude, longitude, days));
            var results = (await Task.WhenAll(tasks)).ToDictionary(x => x.WeatherProviderType, y => y.ForecastDto);

            var primaryForecastDto = InitializeEmptyForecast(days);
            // merge all forecasts into one forecast based on provider priority list
            foreach (var providerType in _appConfiguration.ApiConfiguration.WeatherProvidersPriority)
            {
                if (!results.TryGetValue(providerType, out var secondaryForecastDto)) { continue; }

                MergeForecastDto(primaryForecastDto, secondaryForecastDto);
            }

            // Remove any invalid results
            var validatedForecastResults = primaryForecastDto with
            { HourlyForecast = primaryForecastDto.HourlyForecast.Where(x => _dailyForecastHourDtoValidator.Validate(x).IsValid).ToList() };

            return validatedForecastResults;
        }

        private DailyForecastDto InitializeEmptyForecast(int days)
        {
            var numHours = days * 24;

            var newList = new List<DailyForecastHourDto>();
            for (int i = 0; i < numHours; i++)
            {
                var utcNow = DateTime.UtcNow;
                var adjustedTime = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, 0, 0, utcNow.Kind);
                newList.Add(new DailyForecastHourDto
                {
                    TimeUtc = adjustedTime.AddHours(i)
                }); ;

            }

            var dailyForecastDto = new DailyForecastDto
            {
                HourlyForecast = newList
            };

            return dailyForecastDto;
        }

        private void MergeForecastDto(DailyForecastDto dto1, DailyForecastDto dto2)
        {
            dto1.Latitude = dto1.Latitude == default ? dto2.Latitude : dto1.Latitude;
            dto1.Longitude = dto1.Longitude == default ? dto2.Longitude : dto1.Longitude;

            var forecastHourDtos1 = dto1.HourlyForecast;
            var forecastHourDtos2 = dto2.HourlyForecast;

            MergeForecastHourDto(
                forecastHourDtos1.Any() ? forecastHourDtos1.ToDictionary(x => x.TimeUtc) : forecastHourDtos2.ToDictionary(x => x.TimeUtc),
                forecastHourDtos2.ToDictionary(x => x.TimeUtc));
        }

        private void MergeForecastHourDto(IDictionary<DateTime, DailyForecastHourDto> dtos1, IDictionary<DateTime, DailyForecastHourDto> dtos2)
        {
            foreach (var timeKey in dtos1.Keys)
            {
                var dto1 = dtos1[timeKey];
                if(!dtos2.TryGetValue(timeKey, out var dto2))
                {
                    continue;
                }

                // todo see if automapper can do this? I think it can, but should it?
                dto1.TimeUtc = dto1.TimeUtc == default ? dto2.TimeUtc : dto1.TimeUtc;
                dto1.TemperatureC = dto1.TemperatureC == default ? dto2.TemperatureC : dto1.TemperatureC;
                dto1.TemperatureF = dto1.TemperatureF == default ? dto2.TemperatureF : dto1.TemperatureF;
                dto1.WindSpeedKph = dto1.WindSpeedKph == default ? dto2.WindSpeedKph : dto1.WindSpeedKph;
                dto1.WindDirectionDeg = dto1.WindDirectionDeg == default ? dto2.WindDirectionDeg : dto1.WindDirectionDeg;
                dto1.PrecipitationChance = dto1.PrecipitationChance == default ? dto2.PrecipitationChance : dto1.PrecipitationChance;
            }
        }
    }
}