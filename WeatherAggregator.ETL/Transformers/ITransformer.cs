using System;
using WeatherAggregator.ETL.Dto;

namespace WeatherAggregator.ETL.Transformers;

public interface ITransformer<T>
{
    Task<ForecastDto> Transform(T transformObj);
}
