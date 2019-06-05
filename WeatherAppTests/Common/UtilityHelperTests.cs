using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WeatherApp.Common.Tests
{
    [TestClass()]
    public class UtilityHelperTests
    {
        [TestMethod()]
        public void ReadFromFileCheckIfFiledoesnotExistTest()
        {
            var filepath = @"D:\\Destination";
            var cities = UtilityHelper.ReadFromFile(filepath);
            Assert.IsNull(cities);
        }

        [TestMethod()]
        public void ReadFromFileTest()
        {
            var filepath = @"D:\\Source\CityList.txt";
            IEnumerable<City> cities = UtilityHelper.ReadFromFile(filepath);
            Assert.AreEqual(10, cities.Count());

        }

        [TestMethod()]
        public void CreateDestinationFolder()
        {
            string path = "D:\\Source";
            var directoryInfo = UtilityHelper.CreateDestinationFolder(path);
            var now = DateTime.Now;
            var yearName = now.ToString("yyyy");
            var monthName = now.ToString("MMMM");
            var dayName = now.ToString("dd-MM-yyyy");
            var doesFileExits = Directory.Exists($"{path}\\{yearName}\\{monthName}\\{dayName}");
            Assert.IsTrue(doesFileExits, "Folder created successfully.");

        }

        [TestMethod()]
        public void WriteToJsonFileTest()
        {
            string path = "D:\\Source";
            var directoryInfo = UtilityHelper.CreateDestinationFolder(path);
            var now = DateTime.Now;
            var yearName = now.ToString("yyyy");
            var monthName = now.ToString("MMMM");
            var dayName = now.ToString("dd-MM-yyyy");
            string filepath = $"{path}\\{yearName}\\{monthName}\\{dayName}";


            City city = new City() { Cityname = "London", ZipCode = "2643741" };           

            IWeatherFetcher wf = new WeatherFetcher() { WeatherAPIUrl = "http://api.openweathermap.org", WeatherAPIKey = "aa69195559bd4f88d79f9aadeb77a8f6" }; 
            var currentWeather = wf.GetCurrentWeather(city.ZipCode);
            var destinationFilepath = $"{filepath}\\{city.Cityname}_{city.ZipCode}.txt";
            UtilityHelper.WriteToJsonFile<CurrentWeather>(destinationFilepath, currentWeather, append: false);

            var DoesFileExist=File.Exists(destinationFilepath);

            Assert.IsTrue(DoesFileExist);



        }
    }
}