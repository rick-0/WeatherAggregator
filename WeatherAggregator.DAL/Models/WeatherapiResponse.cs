using Newtonsoft.Json;

namespace WeatherAggregator.DAL.Models;

public record WeatherapiResponse
{
    [JsonProperty("location")]
    public WeatherapiLocation Location { get; set; } = new WeatherapiLocation();
    [JsonProperty("forecast")]
    public WeatherapiForecast Forecast { get; set; } = new WeatherapiForecast();
}

public record WeatherapiLocation
{
    [JsonProperty("lat")]
    public decimal Latitude { get; set; }
    [JsonProperty("lon")]
    public decimal Longitude { get; set; }
}

public record WeatherapiForecast
{
    [JsonProperty("forecastday")]
    public ICollection<WeatherapiForecastDay> ForecastDay { get; set; } = new List<WeatherapiForecastDay>();
}

public record WeatherapiForecastDay
{
    [JsonProperty("date")]
    public DateTime Date { get; set; }
    [JsonProperty("day")]
    public WeatherapiDay Day { get; set; } = new WeatherapiDay();
    [JsonProperty("hour")]
    public ICollection<WeatherapiHour> Hour { get; set; } = new List<WeatherapiHour>();
}

public record WeatherapiDay
{
    [JsonProperty("avgtemp_c")]
    public decimal AvgTempC { get; set; }
    [JsonProperty("avgtemp_f")]
    public decimal AvgTempF { get; set; }
    [JsonProperty("maxwind_kph")]
    public decimal MaxWindKph { get; set; }
    [JsonProperty("daily_chance_of_rain")]
    public int DailyChanceOfRain { get; set; } // 0 = 0% and 100 = 100%
}

public record WeatherapiHour
{
    [JsonProperty("time")]
    public DateTime Time { get; set; }
    [JsonProperty("temp_c")]
    public decimal TempC { get; set; }
    [JsonProperty("temp_f")]
    public decimal TempF { get; set; }
    [JsonProperty("wind_kph")]
    public decimal WindKph { get; set; }
    [JsonProperty("wind_degree")]
    public int WindDegrees { get; set; }
    [JsonProperty("chance_of_rain")]
    public int ChanceOfRain { get; set; } // 0 = 0% and 100 = 100%
}