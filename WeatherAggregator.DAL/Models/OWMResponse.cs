using System.Xml.Serialization;

namespace WeatherAggregator.DAL.Models;

[XmlRoot(ElementName = "weatherdata")]
public record OWMResponse
{
    [XmlElement(ElementName = "location")]
    public OWMLocation Location { get; set; } = new OWMLocation();

    [XmlElement(ElementName = "forecast")]
    public OWMForecast Forecast { get; set; } = new OWMForecast();
}

[XmlRoot(ElementName = "location")]
public record OWMLocation
{

    [XmlElement(ElementName = "location")]
    public OWMCoords Location { get; set; }
}

[XmlRoot(ElementName = "location")]
public record OWMCoords
{
    [XmlAttribute(AttributeName = "latitude")]
    public decimal Latitude { get; set; }

    [XmlAttribute(AttributeName = "longitude")]
    public decimal Longitude { get; set; }
}

[XmlRoot(ElementName = "precipitation")]
public record OWMPrecipitation
{
    [XmlAttribute(AttributeName = "probability")]
    public decimal Probability { get; set; } //  0 and 1, where 0 is equal to 0%, 1 is equal to 100%

    [XmlAttribute(AttributeName = "unit")]
    public string Unit { get; set; } = string.Empty;

    [XmlAttribute(AttributeName = "value")]
    public decimal Value { get; set; }

    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; } = string.Empty;
}

[XmlRoot(ElementName = "windDirection")]
public record OWMWindDirection
{
    [XmlAttribute(AttributeName = "deg")]
    public int Deg { get; set; }

    [XmlAttribute(AttributeName = "code")]
    public string Code { get; set; } = string.Empty;

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; } = string.Empty;
}

[XmlRoot(ElementName = "windSpeed")]
public record OWMWindSpeed
{
    [XmlAttribute(AttributeName = "mps")]
    public decimal MetresPerSecond { get; set; }

    [XmlAttribute(AttributeName = "unit")]
    public string Unit { get; set; } = string.Empty;

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; } = string.Empty;
}

[XmlRoot(ElementName = "temperature")]
public record OWMTemperature
{
    [XmlAttribute(AttributeName = "unit")]
    public string Unit { get; set; } = string.Empty;

    [XmlAttribute(AttributeName = "value")]
    public decimal Value { get; set; }

    [XmlAttribute(AttributeName = "min")]
    public decimal Min { get; set; }

    [XmlAttribute(AttributeName = "max")]
    public decimal Max { get; set; }
}

[XmlRoot(ElementName = "time")]
public record OWMTime
{
    [XmlElement(ElementName = "precipitation")]
    public OWMPrecipitation Precipitation { get; set; } = new OWMPrecipitation();

    [XmlElement(ElementName = "windDirection")]
    public OWMWindDirection WindDirection { get; set; } = new OWMWindDirection();

    [XmlElement(ElementName = "windSpeed")]
    public OWMWindSpeed WindSpeed { get; set; } = new OWMWindSpeed();

    [XmlElement(ElementName = "temperature")]
    public OWMTemperature Temperature { get; set; } = new OWMTemperature();

    [XmlAttribute(AttributeName = "from")]
    public DateTime From { get; set; }

    [XmlAttribute(AttributeName = "to")]
    public DateTime To { get; set; }
}

[XmlRoot(ElementName = "forecast")]
public record OWMForecast
{
    [XmlElement(ElementName = "time")]
    public List<OWMTime> Time { get; set; } = new List<OWMTime>();
}