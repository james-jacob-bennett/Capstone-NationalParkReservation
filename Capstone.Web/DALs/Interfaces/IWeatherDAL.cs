using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DALs.Interfaces
{
    public interface IWeatherDAL
    {
        List<Weather> GetAllWeather();
        List<Weather> GetWeather(string parkCode);
    }
}
