using System.ComponentModel;

namespace WeatherAggregator.Configuration.Enums;

public enum WeatherProviderType
{
    [Description("OWM")]
    OWM,
    [Description("Weatherapi")]
    Weatherapi
}
