using System;
using WeatherAggregator.Configuration.Enums;

namespace WeatherAggregator.Configuration.Configs;

public class ApiConfiguration
{
    public string Abc { get; set; } = string.Empty; // todo actual real config for apikeys etc
    public ICollection<WeatherProviderType> WeatherProvidersPriority { get; set; } = new List<WeatherProviderType>();
}
