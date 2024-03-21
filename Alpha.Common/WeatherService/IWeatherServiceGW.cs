using Refit;

namespace Alpha.Common.WeatherService;

public interface IWeatherServiceGW
{
    [Get("/api/weather")]
    Task<string> Weather();
}
