using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using HangfireLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoggingTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("queue")]
        public ActionResult TestQueue()
        {
            BackgroundJob.Enqueue(() => DoTheThing());

            return Ok();
        }

        [LogToConsole]
        public void DoTheThing()
        {
            _logger.LogInformation("Starting {MethodName}", nameof(DoTheThing));
            var randomGuid = Guid.NewGuid().ToString();
            _logger.LogInformation("Using guid: {Guid} in process", randomGuid);            

            try
            {
                throw new Exception("Something bad happened!");
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, "There was an exception");
            }

            _logger.LogInformation("Completed {MethodName}", nameof(DoTheThing));
            _logger.LogInformation("Completed {MethodName}", nameof(DoTheThing));

        }

    }
}
