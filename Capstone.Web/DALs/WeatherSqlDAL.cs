using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.DALs.Interfaces;
using Capstone.Web.Models;

namespace Capstone.Web.DALs
{
    public class WeatherSqlDAL : IWeatherDAL
    {
        private string _connectionString;

        private const string _sqlGetAllWeather = "SELECT * FROM weather;";

        public WeatherSqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets a list of all the weather and the information in the database.
        /// </summary>
        /// <returns>All weather for all the parks</returns>
        public List<Weather> GetAllWeather()
        {
            List<Weather> result = new List<Weather>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = _sqlGetAllWeather;
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Weather weather = new Weather();
                    weather.ParkCode = Convert.ToString(reader["parkCode"]);
                    weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                    weather.Low = Convert.ToInt32(reader["low"]);
                    weather.High = Convert.ToInt32(reader["high"]);
                    weather.Forecast = Convert.ToString(reader["forecast"]);
                    result.Add(weather);
                }
            }
            return result;
        }
        /// <summary>
        /// Gets the weather information from the database for the specifc Park Code.
        /// </summary>
        /// <param name="parkCode">Park Code</param>
        /// <returns>List of all weather objects for the individual park</returns>
        public List<Weather> GetWeather(string parkCode)
        {
            List<Weather> result = new List<Weather>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                const string sqlGetWeather = "SELECT parkCode, fiveDayForecastValue, low, high, forecast FROM weather WHERE parkCode = @parkCode;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlGetWeather;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Weather weather = new Weather();
                    weather.ParkCode = Convert.ToString(reader["parkCode"]);
                    weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                    weather.Low = Convert.ToInt32(reader["low"]);
                    weather.High = Convert.ToInt32(reader["high"]);
                    weather.Forecast = Convert.ToString(reader["forecast"]);
                    result.Add(weather);
                }
            }
            return result;
        }
    }
}