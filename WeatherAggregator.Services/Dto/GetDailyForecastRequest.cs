namespace WeatherAggregator.Services.Dto;

public record GetDailyForecastRequest
{
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public int Days { get; set; }
}
