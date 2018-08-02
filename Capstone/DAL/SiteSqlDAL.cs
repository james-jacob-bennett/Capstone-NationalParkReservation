using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SiteSqlDAL
    {
        private string connectionString;

        public SiteSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public string SQL_getSites { get; private set; }

        public List<Site> GetSitesInCampground(int campGroundId)
        {
            List<Site> output = new List<Site>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string SQL_getSites = "Select site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities from CampGround.dbo.site where campground_id = @campground_id;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_getSites;
                cmd.Parameters.AddWithValue("@campground_id", campGroundId);
                cmd.Connection = connection;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Site SitesList = new Site();

                    SitesList.Id =  Convert.ToInt32(reader["site_id"]);
                    SitesList.CampGround_Id = Convert.ToInt32(reader["campground_id"]);
                    SitesList.SiteNumber = Convert.ToInt32(reader["site_number"]);
                    SitesList.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                    SitesList.Accessible = Convert.ToBoolean(reader["accessible"]);
                    SitesList.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                    SitesList.Utilities = Convert.ToBoolean(reader["utilities"]);

                    output.Add(SitesList);
                }

                return output;
            }
        }

        //public bool DateRangeSubMenu(int campgrooundID)
        //{
        //    Park park = new Park();
        //    Console.Clear();
        //    Console.WriteLine("{0, -20}{1, -20}{2, -20}{3, -20}", "      Name", "      Open", "   Close", "  Daily Fee");
        //    ProjectCLI.PrintCampGrounds(park.Park_Id);
        //    Console.WriteLine();
        //    Console.Write($"Which Campground (enter 0 to return to main menu)?"); string campgroundSelection = Console.ReadLine();
        //    Console.Write("What is your arrival date? (mm/dd/yyyy)"); string arrivalSelection = Console.ReadLine();
        //    Console.Write("What is your departure date? (mm/dd/yyyy)"); string departureSelection = Console.ReadLine();
        //    int dawd = int.Parse(campgroundSelection);
        //    if (dawd == 0)
        //    {
        //        Console.Clear();

        //        RunCLI();
        //    }
        //    return false;
        //}

    }
}
