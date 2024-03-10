using Refit;

namespace Alpha.Common.WeatherService;

public interface IWeatherService
{
    [Get("/api/weather")]
    Task<List<WeatherForecast>> Weather();
}
