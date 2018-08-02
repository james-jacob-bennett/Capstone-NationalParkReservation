using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DALs.Interfaces
{
    public interface IParksDAL
    {
        List<ParkHomeView> GetAllParkHomeViews();
        Park GetPark(string parkCode);
    }
}
