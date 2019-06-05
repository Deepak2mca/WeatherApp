using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Common;

namespace WeatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
                var path = ConfigurationManager.AppSettings["SourceFilePath"];
                IEnumerable<City> cities = UtilityHelper.ReadFromFile(path + "\\" + "CityList.txt");
                Parallel.ForEach(cities, (currentcity) =>
                {
                    IWeatherFetcher wf = new WeatherFetcher();
                    var currentWeather = wf.GetCurrentWeather(currentcity.ZipCode);
                    var dirInfo = UtilityHelper.CreateDestinationFolder(ConfigurationManager.AppSettings["DestinationPath"]);
                    UtilityHelper.WriteToJsonFile<CurrentWeather>($"{dirInfo.FullName} \\ {currentcity.Cityname}_{currentcity.ZipCode} .txt", currentWeather, append: false);

                }
               );
                Console.ReadLine();
            
        }
    }
}
