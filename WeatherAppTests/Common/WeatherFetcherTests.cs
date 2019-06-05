using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace WeatherApp.Common.Tests
{
    [TestClass()]
    public class WeatherFetcherTests
    {
        IWeatherFetcher wf = new WeatherFetcher() { WeatherAPIUrl = "http://api.openweathermap.org", WeatherAPIKey = "aa69195559bd4f88d79f9aadeb77a8f6" };

        //NameValueCollection appSettings = ConfigurationManager.AppSettings;
        [TestMethod()]
        public void GetCurrentWeatherTest()
        {          
            var currentWeather = wf.GetCurrentWeather("2643741");
            Assert.IsNotNull(currentWeather, $"The temp in {currentWeather.name} is {currentWeather.main.temp}.");
        }
    }
}