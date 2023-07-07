using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WeatherAggregator.Configuration;
using WeatherAggregator.DAL.Models;
using WeatherAggregator.DAL.Queries;
using WeatherAggregator.Services;
using WeatherAggregator.Services.Dto;
using WeatherAggregator.Services.ETL;
using WeatherAggregator.Services.ETL.Transformers;
using WeatherAggregator.Services.Validators;

namespace WeatherAggregator.IoC
{
    public class CorePackage
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAppConfiguration, AppConfiguration>();

            serviceCollection.AddSingleton<IWeatherService, WeatherService>();

            serviceCollection.AddSingleton<IWeatherQuery<WeatherapiResponse>, WeatherapiQuery>();
            serviceCollection.AddSingleton<IWeatherQuery<OWMResponse>, OWMQuery>();

            serviceCollection.AddSingleton<IWeatherAggregator<DailyForecastDto>, DailyForecastWeatherAggregator>();
            serviceCollection.AddSingleton<IApiTransformer<DailyForecastDto>, OWMTransformer>();
            serviceCollection.AddSingleton<IApiTransformer<DailyForecastDto>, WeatherapiTransformer>();

            serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            serviceCollection.AddSingleton<IValidator<DailyForecastHourDto>, DailyForecastHourDtoValidator>();
            serviceCollection.AddSingleton<IValidator<GetDailyForecastRequest>, GetDailyForecastRequestValidator>();
        }
    }
}