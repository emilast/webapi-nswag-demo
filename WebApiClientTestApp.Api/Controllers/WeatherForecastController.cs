using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiClientTestApp.Api.Contracts;

namespace WebApiClientTestApp.Api.Controllers
{
    /// <summary>
    /// API for retrieving weather forecasts.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get the weather forecast for today and 5 days forward.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("fivedays")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, type: typeof(IEnumerable<WeatherForecast>))]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.InternalServerError, type: typeof(ApiError))]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            try
            {
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = GetRandomSummary(rng)
                }).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiError { Message = "Server error" });
            }
        }

        private static WeatherSummary GetRandomSummary(Random rng)
        {
            var valueCount = Enum.GetNames(typeof(WeatherSummary)).Length;
            return Enum.GetValues(typeof(WeatherSummary)).Cast<WeatherSummary>().Skip(rng.Next(valueCount)).First();
        }
    }
}
