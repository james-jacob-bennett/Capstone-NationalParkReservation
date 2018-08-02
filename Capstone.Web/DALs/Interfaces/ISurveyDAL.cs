using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DALs
{
    public interface ISurveyDAL
    {
        List<SurveyResult> GetAllSurveys();
        bool SaveSurvey(SurveyResult newSurvey);
        List<SurveyFavoritePark> GetSurveyFavoriteParks();
    }
}