using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.DALs.Interfaces;
using System.Data.SqlClient;

namespace Capstone.Web.DALs
{
    public class ParksSqlDAL : IParksDAL
    {
        private string _connectionString;

        private const string _sqlGetAllParkHomeViews = "SELECT parkCode, parkName, state, " +
                                                        "parkDescription FROM park;";

        public ParksSqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Retrieves all the parks from the database for our home page
        /// </summary>
        /// <returns>returns list of ParkHomeView objects</returns>
        public List<ParkHomeView> GetAllParkHomeViews()
        {
            List<ParkHomeView> result = new List<ParkHomeView>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = _sqlGetAllParkHomeViews;
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ParkHomeView park = new ParkHomeView();
                    park.ParkCode = Convert.ToString(reader["parkCode"]);
                    park.ParkName = Convert.ToString(reader["parkName"]);
                    park.State = Convert.ToString(reader["state"]);
                    park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                    result.Add(park);
                }
            }
            return result;
        }
        /// <summary>
        /// This method is getting the information from the database with the information
        /// from the individual park based on the parkCode
        /// </summary>
        /// <param name="parkCode">park code string</param>
        /// <returns>returns Park object based on park code entered </returns>
        public Park GetPark(string parkCode)
        {
            Park result = new Park();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                const string sqlGetPark = "SELECT parkCode, parkName, state, acreage, elevationInFeet, milesOfTrail, numberOfCampsites, climate," +
                                          " yearFounded, annualVisitorCount, inspirationalQuote, inspirationalQuoteSource, parkDescription," +
                                          " entryFee, numberOfAnimalSpecies FROM Park WHERE parkCode = @parkCode;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlGetPark;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.ParkCode = Convert.ToString(reader["parkCode"]);
                    result.ParkName = Convert.ToString(reader["parkName"]);
                    result.State = Convert.ToString(reader["state"]);
                    result.Acreage = Convert.ToInt32(reader["acreage"]);
                    result.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                    result.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                    result.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                    result.Climate = Convert.ToString(reader["climate"]);
                    result.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                    result.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                    result.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                    result.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                    result.ParkDescription = Convert.ToString(reader["parkDescription"]);
                    result.EntryFee = Convert.ToInt32(reader["entryFee"]);
                    result.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                }
            }
            return result;
        }
    }
}