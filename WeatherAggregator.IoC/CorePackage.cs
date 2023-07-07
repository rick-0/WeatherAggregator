using Microsoft.Extensions.DependencyInjection;
using WeatherAggregator.Configuration;
using WeatherAggregator.DAL.Models;
using WeatherAggregator.DAL.Queries;
using WeatherAggregator.Services;
using WeatherAggregator.Services.ETL;
using WeatherAggregator.Services.ETL.Dto;
using WeatherAggregator.Services.ETL.Transformers;

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

            serviceCollection.AddSingleton<IWeatherAggregator<DailyForecastHourDto>, DailyForecastWeatherAggregator>();
            serviceCollection.AddSingleton<IApiTransformer, OWMTransformer>();
            serviceCollection.AddSingleton<IApiTransformer, WeatherapiTransformer>();

            serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}