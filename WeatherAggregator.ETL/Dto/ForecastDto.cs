using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator.ETL.Dto;

public class ForecastDto
{
    public decimal Latitude;
    public decimal Longitude;
    public DateTime TimeUtc;
    public decimal TemperatureCelcius;
    public decimal TemperatureFahrenheit;
    public decimal WindSpeed;
    public decimal WindDirection;
    public decimal PrecipitationChance;
}
