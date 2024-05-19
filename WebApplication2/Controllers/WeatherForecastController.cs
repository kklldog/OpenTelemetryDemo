using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Resources;
using System.Diagnostics;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly  ActivitySource _source;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _source = new ActivitySource("MyTraceSample", "1.0.0");
        }

        [HttpGet]
        public async Task<string> Get([FromQuery]string city)
        {
            _logger.LogInformation("Hello WeatherForecast");

            using (var activity = _source.StartActivity("CallWeatherForecast")) {

                activity?.AddTag("city", city);

                await Task.Delay(100);

                await GetWeatherInfoFromWebservice();

                await FormatWeatherInfo();
            }

            return "24¡ãc";
        }

        async Task GetWeatherInfoFromWebservice()
        {
            using (var activity = _source.StartActivity("GetWeatherInfoFromWebservice"))
            {
                await Task.Delay(200);
            }
        }

        async Task FormatWeatherInfo()
        {
            using (var activity = _source.StartActivity("FormatWeatherInfo"))
            {
                await Task.Delay(300);
            }
        }
    }
}
