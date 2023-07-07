using AutoMapper;
using WeatherAggregator.DAL.Models;
using WeatherAggregator.Services.ETL.Dto;

namespace WeatherAggregator.Services.MappingProfile;

public class ServiceMappingProfile : Profile
{
    public ServiceMappingProfile()
    {
        CreateMap<WeatherapiHour, DailyForecastHourDto>()
            .ForMember(dest => dest.TemperatureC, opt => opt.MapFrom(src => src.TempC))
            .ForMember(dest => dest.TemperatureF, opt => opt.MapFrom(src => src.TempF))
            .ForMember(dest => dest.WindSpeedKph, opt => opt.MapFrom(src => src.WindKph))
            .ForMember(dest => dest.WindDirectionDeg, opt => opt.MapFrom(src => src.WindDegrees))
            .ForMember(dest => dest.PrecipitationChance, opt => opt.MapFrom(src => src.ChanceOfRain))
            .ForMember(dest => dest.TimeUtc, opt => opt.MapFrom(src => src.Time));

        CreateMap<OWMTime, DailyForecastHourDto>()
            .ForMember(dest => dest.TemperatureC, opt => opt.MapFrom(src => src.Temperature.Value))
            .ForMember(dest => dest.TemperatureF, opt => opt.MapFrom(src => (src.Temperature.Value * 1.8m) + 32)) // °F = (°C x 1.8) + 32
            .ForMember(dest => dest.WindSpeedKph, opt => opt.MapFrom(src => src.WindSpeed.MetresPerSecond * 3.6m))
            .ForMember(dest => dest.WindDirectionDeg, opt => opt.MapFrom(src => src.WindDirection.Deg))
            .ForMember(dest => dest.PrecipitationChance, opt => opt.MapFrom(src => src.Precipitation.Probability * 100m))
            .ForMember(dest => dest.TimeUtc, opt => opt.MapFrom(src => src.From));
    }
}
