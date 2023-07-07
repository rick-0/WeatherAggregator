namespace WeatherAggregator.Services.Dto;

public record DailyForecastDto
{
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public ICollection<DailyForecastHourDto> HourlyForecast { get; set; } = new List<DailyForecastHourDto>();
}
