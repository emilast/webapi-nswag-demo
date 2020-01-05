using System;
using System.Text.Json.Serialization;
using WebApiClientTestApp.Api.Serialization;

namespace WebApiClientTestApp.Api.Contracts
{
    /// <summary>
    /// Holds weather forecast data for a date.
    /// </summary>
    public class WeatherForecast
    {
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public decimal TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public WeatherSummary Summary { get; set; }
    }
}
