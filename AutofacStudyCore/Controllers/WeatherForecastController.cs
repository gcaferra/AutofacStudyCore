using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutofacStudyCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutofacStudyCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ITestService1 _testService1;
        private readonly ITestService2 _testService2;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITestService1 testService1, ITestService2 testService2)
        {
            _logger = logger;
            _testService1 = testService1;
            _testService2 = testService2;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _testService1.NotWorkingMethod();
            _testService2.WorkingMethod();
            
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}