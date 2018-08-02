using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Common.WebHost;
using Capstone.Web.DALs;
using Capstone.Web.DALs.Interfaces;


namespace Capstone.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            string connectionString = ConfigurationManager.ConnectionStrings["NPGeek"].ConnectionString;
            kernel.Bind<IParksDAL>().To<ParksSqlDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<ISurveyDAL>().To<SurveySqlDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IWeatherDAL>().To<WeatherSqlDAL>().WithConstructorArgument("connectionString", connectionString);

            return kernel;
        }
    }
}
