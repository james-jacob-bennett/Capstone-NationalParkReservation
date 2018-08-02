using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DALs;
using Capstone.Web.DALs.Interfaces;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParksDAL _dal;
        private IWeatherDAL _weatherdal;

        public HomeController(IParksDAL dal, IWeatherDAL dal2)
        {
            _dal = dal;
            _weatherdal = dal2;
        }
        

        // GET: Home
        public ActionResult Index()
        {
            List<ParkHomeView> parks = _dal.GetAllParkHomeViews();
            return View("Index", parks);
        }
        //GET: Detail Page
        public ActionResult Detail(string parkCode)
        {
            if(parkCode == null)
            {
                parkCode = "CVNP";
            }
            Park park = _dal.GetPark(parkCode);
            park.Forecast = _weatherdal.GetWeather(parkCode);
            park.TempHelper = GetActiveTempHelper();
            return View("Detail", park );
        }
       //Changing user preference for celsius or farenheit
        public ActionResult ChangeTempStyle(TempScale TempScale, string parkCode)
        {
            //check session dictionary for active temphelper
            TempHelper helper = GetActiveTempHelper();
            //set temperature preference bool
            helper.IsFarenheit = TempScale == TempScale.Fahrenheit;
            //redirect back to detail page with same parkCode
            return Detail(parkCode);
        }


        //Check session for temphelper and add if not currently in session
        private TempHelper GetActiveTempHelper()
        {
            TempHelper tempHelper = null;

            // See if the user has a Temp Helper stored in session
            if (Session["Temp_Helper"] == null)
            {
                tempHelper = new TempHelper();
                Session["Temp_Helper"] = tempHelper;        // <-- saves the Temp Helper into session
            }
            else
            {
                tempHelper = Session["Temp_Helper"] as TempHelper; // <-- gets the Temp Helper out of session
            }

            // If not, create one for them

            return tempHelper;
        }

    }
}