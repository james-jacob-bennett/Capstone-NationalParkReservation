using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DALs
{
    public class SurveySqlDAL : ISurveyDAL
    {
        private string _connectionString;

        private const string _sqlGetAllReviews = "Select * from survey_result";
        private const string _sqlGetSurveyFavoriteParks = "SELECT  count(surveyId) as votes, survey_result.parkCode as parkCode, Park.parkName as parkName, Park.state as state" +
                                                          " FROM survey_result" +
                                                          " JOIN Park on survey_result.parkCode = Park.parkCode" +
                                                          " group by survey_result.parkCode, Park.parkName, Park.state" +
                                                          " ORDER BY votes DESC;";

        public SurveySqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// Gets all the survey information from the survey_result in the database.
        /// </summary>
        /// <returns>Returns a list of all submitted surveys in database</returns>
        public List<SurveyResult> GetAllSurveys()
        {
            List<SurveyResult> result = new List<SurveyResult>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = _sqlGetAllReviews;
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SurveyResult survey = new SurveyResult();
                    survey.SurveyId = Convert.ToInt32(reader["surveyId"]);
                    survey.ParkCode = Convert.ToString(reader["parkCode"]);
                    survey.EmailAddress = Convert.ToString(reader["emailAddress"]);
                    survey.State = Convert.ToString(reader["state"]);
                    survey.ActivityLevel = Convert.ToString(reader["activityLevel"]);
                    result.Add(survey);
                }
            }
            return result;
        }
        /// <summary>
        /// Inserting the user information they entered on the site into our database.
        /// </summary>
        /// <param name="newSurvey">survey to be added to database</param>
        /// <returns>Inserts survey information into the database</returns>
        public bool SaveSurvey(SurveyResult newSurvey)
        {

            bool wasSuccessful = true;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                const string sqlSavePost = "INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel )" +
                                            "VALUES ( @parkcode, @emailAddress, @state, @activityLevel);";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlSavePost;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@parkcode", newSurvey.ParkCode);
                cmd.Parameters.AddWithValue("@emailAddress", newSurvey.EmailAddress);
                cmd.Parameters.AddWithValue("@state", newSurvey.State);
                cmd.Parameters.AddWithValue("@activityLevel", newSurvey.ActivityLevel);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    wasSuccessful = false;
                    throw new Exception("ERROR! your survey was not sent, Please try again.");
                }
            }
            return wasSuccessful;
        }
        
        /// <summary>
        /// Retrieves the information we need for the amount of surveys for the parks.
        /// </summary>
        /// <returns>List of survey objects for result view</returns>
        public List<SurveyFavoritePark> GetSurveyFavoriteParks()
        {
            List<SurveyFavoritePark> result = new List<SurveyFavoritePark>();
            

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = _sqlGetSurveyFavoriteParks;
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SurveyFavoritePark survey = new SurveyFavoritePark();
                    survey.NumberOfVotes = Convert.ToInt32(reader["votes"]);
                    survey.ParkCode = Convert.ToString(reader["parkCode"]);
                    survey.ParkName = Convert.ToString(reader["parkName"]);
                    survey.NameOfState = Convert.ToString(reader["state"]);
                    
                    result.Add(survey);
                }
            }
            return result;
        }
    }
}