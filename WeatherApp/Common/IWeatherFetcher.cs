using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Common
{
   public interface IWeatherFetcher
    {
        CurrentWeather GetCurrentWeather(string zipCode);
    }
}
