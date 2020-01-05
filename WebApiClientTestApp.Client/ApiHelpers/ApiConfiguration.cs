using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiClientTestApp.Client.ApiHelpers
{
    public class ApiConfiguration
    {
        public string WeatherApiBaseUrl { get; set; }

        public ApiConfiguration()
        {
            // TODO: Fetch from settings file
            WeatherApiBaseUrl = "http://localhost:5000";
        }
    }
}
