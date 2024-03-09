using Refit;

namespace Alpha.Common.WeatherService;

public interface IWeatherService
{
    [Get("/api/weather")]
    List<WeatherForecast> Get();
}
