using System;
using System.Threading.Tasks;
using WebApiClientTestApp.Client.ApiHelpers;
using WebApiClientTestApp.Client.WeatherApi;

namespace WebApiClientTestApp.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("5 day forecast:");

            var client = new WeatherClient(new ApiConfiguration());
            var forecasts = await client.FivedaysAsync();
            foreach (var forecast in forecasts)
            {
                Console.WriteLine($"{forecast.Date}: {forecast.TemperatureC} {forecast.TemperatureF} {forecast.Summary}");
            }

            Console.ReadKey();
        }
    }
}
