namespace WeatherAggregator.Services.Dto;

public record DailyForecastHourDto
{
    public decimal? TemperatureC { get; set; }
    public decimal? TemperatureF { get; set; }
    public decimal? WindSpeedKph { get; set; }
    public decimal? WindDirectionDeg { get; set; }
    public decimal? PrecipitationChance { get; set; }
    public DateTime TimeUtc { get; set; } = new DateTime();
}
