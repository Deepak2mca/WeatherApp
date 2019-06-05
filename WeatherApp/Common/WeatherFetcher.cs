using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WeatherApp.Common
{
   public class WeatherFetcher : IWeatherFetcher
    {
        private HttpClient client = new HttpClient();

        public string WeatherAPIUrl { get; set; } = ConfigurationManager.AppSettings["WeatherAPI"];
        public string WeatherAPIKey { get; set; } = ConfigurationManager.AppSettings["WeatherAPIKeyCode"];

        public CurrentWeather GetCurrentWeather(string zipCode)
        {
            var json = RunAsync(WeatherAPIKey, zipCode).GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<CurrentWeather>(json);
        }

       
        private async Task<string> RunAsync(string key, string zipCode)
        {            
            client.BaseAddress = new Uri(WeatherAPIUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = "";
            try
            {
                result = await GetWeatherAsync(key, zipCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        private async Task<string> GetWeatherAsync(string key, string zipCode)
        {
            var result = "";
            string url = $"/data/2.5/weather?id={zipCode}&appid={key}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            else
            {
                // dump any errors to the screen 
                // we can use logger 
                Console.WriteLine(response.ToString());
            }
            return result;
        }
    }
}
