using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ParkSqlDAL
    {
        private string connectionString;

        public ParkSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Park> GetAllParks()
        {
            List<Park> output = new List<Park>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string SQL_getParks = "Select * from park;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_getParks;
                cmd.Connection = connection;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Park parkList = new Park();

                    parkList.Park_Id = Convert.ToInt32(reader["park_id"]);
                    parkList.Name = Convert.ToString(reader["name"]);
                    parkList.Location = Convert.ToString(reader["location"]);
                    parkList.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                    parkList.Area = Convert.ToInt32(reader["area"]);
                    parkList.Visitors = Convert.ToInt32(reader["visitors"]);
                    parkList.Description = Convert.ToString(reader["description"]);


                    output.Add(parkList);

                }

                return output;

            }

        }

        public List<Park> ReturnPark(int park_id)
        {
            List<Park> desiredPark = new List<Park>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                const string sqlParkSearch = "SELECT * FROM Park WHERE @park_id = park_id ";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlParkSearch;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@command", park_id);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Park park = new Park();

                    park.Name = Convert.ToString(reader["name"]);
                    park.Location = Convert.ToString(reader["location"]);
                    park.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                    park.Area = Convert.ToInt32(reader["area"]);
                    park.Visitors = Convert.ToInt32(reader["visitors"]);
                    park.Description = Convert.ToString(reader["description"]);

                    desiredPark.Add(park);
                    
                }
                return desiredPark;

            }

        }

    }
}
