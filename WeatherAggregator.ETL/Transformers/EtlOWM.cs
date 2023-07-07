using WeatherAggregator.DAL.Models;
using WeatherAggregator.ETL.Dto;

namespace WeatherAggregator.ETL.Transformers
{
    public class EtlOWM : ITransformer<OWMResponse>
    {
        public Task<ForecastDto> Transform(OWMResponse transformObj)
        {
            throw new NotImplementedException();
        }
    }
}