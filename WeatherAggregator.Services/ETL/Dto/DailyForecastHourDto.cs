namespace WeatherAggregator.Services.ETL.Dto;

public record DailyForecastHourDto
{
    public decimal? Latitude; // todo move lat long outside
    public decimal? Longitude;
    public decimal? TemperatureC;
    public decimal? TemperatureF;
    public decimal? WindSpeedKph;
    public decimal? WindDirectionDeg;
    public decimal? PrecipitationChance;
    public DateTime? TimeUtc;
}
