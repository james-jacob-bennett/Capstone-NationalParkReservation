using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DALs;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private ISurveyDAL _dal;
        public SurveyController(ISurveyDAL dal)
        {
            _dal = dal;
        }

        // GET: Survey
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Survey(SurveyResult model)
        {
            ActionResult result;

            //Validate the model before proceeding
            if (!ModelState.IsValid)
            {
                result = View("Index", model);
            }
            else
            {
                
                _dal.SaveSurvey(model);
                result = RedirectToAction("SurveyConfirmation", "Survey");
            }
            return result;
        }

        public ActionResult SurveyConfirmation()
        {
            List<SurveyFavoritePark> surveys = _dal.GetSurveyFavoriteParks();
            return View("SurveyConfirmation", surveys);
        }
    }
}