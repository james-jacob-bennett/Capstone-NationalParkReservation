using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }

        public double CelsiusLow => Math.Round((Low - 32.0) * .5556);
        public double CelsiusHigh => Math.Round((High - 32.0) * .5556);

        

    }

    public enum TempScale
    {
        Fahrenheit,
        Celsius,
        Kelvin,
        DeLisle,
        Rankine
    }

}