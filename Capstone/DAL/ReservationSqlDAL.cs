using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ReservationSqlDAL
    {
        private string connectionString;

        public ReservationSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
            
        }

        public string SQL_getReservation { get; private set; }

        //public List<Reservation> GetAllReservation()
        //{
        //    List<Reservation> output = new List<Reservation>();

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        const string SQL_getReservation = "Select * from reservation;";

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = SQL_getReservation;
        //        cmd.Connection = connection;

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            Reservation ReservationsList = new Reservation();

        //            ReservationsList.Id = Convert.ToInt32(reader["reservation_id"]);
        //            ReservationsList.Site_Id = Convert.ToInt32(reader["site_id"]);
        //            ReservationsList.Name = Convert.ToString(reader["name"]);
        //            ReservationsList.FromDate = Convert.ToDateTime(reader["from_date"]);
        //            ReservationsList.ToDate = Convert.ToDateTime(reader["to_date"]);
        //            ReservationsList.Create_Date = Convert.ToDateTime(reader["create_date"]);


        //            output.Add(ReservationsList);
        //        }

        //        return output;
        //    }
        //}

        public List<Reservation> GetReservationsinSite(int siteId)
        {
            List<Reservation> output = new List<Reservation>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string SQL_getReservation = "Select reservation_id, site_id, name, from_date, to_date, create_date from CampGround.dbo.reservation where site_id = @site_id;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_getReservation;
                cmd.Parameters.AddWithValue("@site_id", siteId);
                cmd.Connection = connection;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Reservation reservationsList = new Reservation();

                    reservationsList.Id = Convert.ToInt32(reader["reservation_id"]);
                    reservationsList.Site_Id = Convert.ToInt32(reader["site_id"]);
                    reservationsList.Name = Convert.ToString(reader["name"]);
                    reservationsList.FromDate = Convert.ToDateTime(reader["from_date"]);
                    reservationsList.ToDate = Convert.ToDateTime(reader["to_date"]);
                    reservationsList.Create_Date = Convert.ToDateTime(reader["create_date"]);
                    

                    output.Add(reservationsList);
                }

                return output;
            }
        }

        public List<Site> ReturnSites(int campGroundSelection, DateTime arrivalDate, DateTime departureDate)
        {
            List<Site> siteList = new List<Site>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                const string siteSearch = "select top(5)site.* from site " +
                                          "JOIN campground on site.campground_id = campground.campground_id " +
                                          "where site.campground_id = @campGroundSelection " +
                                          "and site_id not in(select site_id from reservation " +
                                          "where @arrivalDate <= to_date " +
                                          "and @departureDate >= from_date);";
                SqlCommand cmd = new SqlCommand();  
                cmd.CommandText = siteSearch;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@campGroundSelection", campGroundSelection);
                cmd.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                cmd.Parameters.AddWithValue("@departureDate", departureDate);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Site site = new Site();

                    site.SiteNumber = Convert.ToInt32(reader["site_number"]);
                    site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                    site.Accessible = Convert.ToBoolean(reader["accessible"]);
                    site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                    site.Utilities = Convert.ToBoolean(reader["utilities"]);

                    siteList.Add(site);
                }
                return siteList;
            }
        }

        public bool InsertReservation(int siteID, string resName, DateTime fromDate, DateTime toDate)
        {
            bool wasSuccesful = true;
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                const string SQL_insert = "insert into reservation (site_id, name, from_date, to_date)" +
                                          "values (@siteID, @resName, @fromDate, @toDate);";

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = SQL_insert;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@siteID", siteID);
                cmd.Parameters.AddWithValue("@resName", resName);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);

                int rowsAffected = cmd.ExecuteNonQuery();
                //int id = (int)cmd.ExecuteScalar();
                if (rowsAffected == 0)
                {
                    wasSuccesful = false;
                }

            }
            return wasSuccesful;
        }
        public int GetReservationID(int siteID, string resName, DateTime fromDate, DateTime toDate)
        {
            int resID = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                const string SQLresIDSearch = "select * FROM reservation WHERE site_id = @siteID and name = @resName and from_date = @fromDate and to_date = @toDate";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLresIDSearch;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@siteID", siteID);
                cmd.Parameters.AddWithValue("@resName", resName);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Reservation res = new Reservation();

                    resID = Convert.ToInt32(reader["reservation_id"]);
                }
            }
            return resID;
        }
    }

    
}
