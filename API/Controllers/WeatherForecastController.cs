// this is an example API controller. an API controller will contain routes and endpoints

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

// the brackets indicate attributes.
[ApiController]
// [controller] is a placeholder for the actual name of the controller (localhost:5000/weatherforecast. gets its name from the name of the controller below, minus the word Controller.)
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    // this is an example of Dependency Injection. we're making something (the logger) available for our controller 
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    // here we have the endpoint. 
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
