using System;

namespace WebApiClientTestApp.Client.WeatherApi
{
    public partial class WeatherClient
    {
        protected override Uri GetBaseAddress()
        {
            return new Uri(Configuration.WeatherApiBaseUrl);
        }
    }
}
