using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class TempHelper
    {
        public bool IsFarenheit { get; set; } = true;
        /// <summary>
        /// Dictionary to compare forecast string to recommendation string.
        /// </summary>
        public Dictionary<string, string> _recommendations = new Dictionary<string, string>()
        {
            { "partly cloudy", "" },
            { "rain", "Pack Rain Gear and Water Proof Shoes. " },
            { "cloudy", "" },
            { "snow", "Pack Snowshoes. " },
            { "sunny", "Pack sunblock. " },
            { "thunderstorms", "Seek shelter and avoid hiking on exposed ridges. " }
        };
    

        /// <summary>
        /// Returns the recommendations based on the weather object.
        /// </summary>
        /// <param name="weather">Weather</param>
        /// <returns>String of recommendations</returns>
        public string GetWeatherRecommendation(Weather weather)
        {
            string result = _recommendations[weather.Forecast];
            if(weather.High>75)
            {
                result += "Bring an extra gallon of water. ";
            }
            if ((weather.High - weather.Low)>20)
            {
                result += "Wear breathable layers ";
            }
            if (weather.Low<20)
            {
                result += "DANGER: frigid temperatures, be careful of exposure. ";
            }

            if(result == "")
            {
                result = "It's a nice day, be smart!";
            }
            return result;
        }
        /// <summary>
        /// SelectListItem for Fahrenheit/Celsius bool.
        /// </summary>
        public static List<SelectListItem> TrueFalse = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Fahrenheit", Value="True" },
            new SelectListItem() { Text = "Celsius", Value = "False" }
        };
    }
}