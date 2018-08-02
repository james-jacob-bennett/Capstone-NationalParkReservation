using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;
using System.Data.SqlClient;

namespace Capstone

{
    public class ProjectCLI
    {
        //refer to parksubmenu!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        const string DatabaseConnection = @"Data Source =.\SQLEXPRESS;Initial Catalog = CampGround; Integrated Security = True";
        const string Command_Quit = "q";
        public Dictionary<int, Park> _parkDictionary = new Dictionary<int, Park>();
        public Dictionary<int, Campground> _campgroundDictionary = new Dictionary<int, Campground>();

        public void RunCLI()
        {
            bool done = false;
            while (!done)
            {
                Console.Clear();
                PrintMenu();
                string tempuserInput = Console.ReadLine();
                if (tempuserInput == "q")
                {
                    done = true;
                }
                else if (_parkDictionary.ContainsKey(int.Parse(tempuserInput)))
                {
                    var park = _parkDictionary[int.Parse(tempuserInput)];
                    ParkSubMenu(park);
                }
                else
                {
                    Console.WriteLine("Please enter valid input");
                }
            }
        }

        //public void ParkInfo(string parkId)
        //{
        //    int park_id = int.Parse(parkId);
        //    ParkSqlDAL park = new ParkSqlDAL(DatabaseConnection);
        //    park.ReturnPark(park_id);
        //    Console.WriteLine();



        //    Console.WriteLine("");
        //}

        public void PrintMenu()
        {
            Console.WriteLine("Welcome to the Parks Interface...");
            Console.WriteLine("Select a park for further details...");
          
            PrintParks();
            Console.WriteLine("q) Quit");
        }

        public void ParkSubMenu(Park park)
        {
            bool isDone = false;
            while (!isDone)
            {
                Console.Clear();
                Console.WriteLine(park.Name + " National Park");
                Console.WriteLine($"Location: {park.Location}");
                Console.WriteLine($"Established: {park.EstablishDate}");
                Console.WriteLine($"Area: {park.Area}");
                Console.WriteLine($"Annual visitors: {park.Visitors}");
                Console.WriteLine();
                Console.WriteLine(park.Description);
                
                Console.WriteLine("Please Select a Command");
                Console.WriteLine();
                Console.WriteLine($"1) View Campgrounds");
                Console.WriteLine($"2) Return to Previous Screen");

                string userInput = Console.ReadLine();
                if ((userInput) == "2")
                {
                    isDone = true;
                }
                else if (userInput == "1")
                {
                    SubSubMenu(park);
                }
                isDone = true;
            }
        }

        public void PrintParks()
        {
            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
            List<Park> parks = dal.GetAllParks();
            _parkDictionary.Clear();
            //dictionary should contain park value as  key, park info
            for (int i = 1; i <= parks.Count; i++)
            {
                Park park = parks[i-1];
                Console.WriteLine($"{i}){parks[i-1].Name} National Park");
                _parkDictionary.Add(i, park);
            }
        }
        public void PrintCampGrounds(int parkId)
        {
            CampGroundSqlDAL dal = new CampGroundSqlDAL(DatabaseConnection);
            List<Campground> campgrounds = dal.GetCampGroundsInPark(parkId);
            _campgroundDictionary.Clear();
            for (int i = 1; i <= campgrounds.Count; i++)
            {
                Campground campground = campgrounds[i - 1];
                Console.WriteLine($"({campgrounds[i - 1].Id}){campgrounds[i - 1].Name,-20}  {campgrounds[i - 1].OpenFromMonthstr,-15} {campgrounds[i - 1].OpenToMonthstr,-20} {campgrounds[i - 1].DailyFee.ToString("c"),-20}");
                _campgroundDictionary.Add(i, campground);
            }
        }
       

        public void DateRangeSubMenu(int parkID)
        {
            Console.Clear();
            Console.WriteLine("{0, -20}{1, -20}{2, -20}{3, -20}", "      Name", "      Open", "   Close", "  Daily Fee");
            PrintCampGrounds(parkID);
            Console.WriteLine();
            Console.Write($"Which Campground (enter 0 to return to main menu)?");
            string campgroundSelection = Console.ReadLine();
            int selection = int.Parse(campgroundSelection);
            int resID = 0;
            if (_campgroundDictionary.ContainsKey(selection))
            {
                Campground campground = _campgroundDictionary[selection];

                Console.Write("What is your arrival date? (yyyy/mm/dd)");
                DateTime arrivalDate = DateTime.Parse(Console.ReadLine());
                Console.Write("What is your departure date? (yyyy/mm/dd)");
                DateTime departureDate = DateTime.Parse(Console.ReadLine());   
                SiteSqlDAL dal = new SiteSqlDAL(DatabaseConnection);
                var listOfSites = dal.GetSitesInCampground(campground.Id);
                ReservationSqlDAL resDal = new ReservationSqlDAL(DatabaseConnection);
                List<Site> availableSites = resDal.ReturnSites(selection, arrivalDate, departureDate);
                Console.WriteLine();
                Console.WriteLine("Results matching your search criteria");
                Console.WriteLine("{0, -15}{1, -15}{2, -15}{3, -15}{4, -20}", "Site Number", "Max Occupancy", "Accesibility", "Max RV Length", "Utilities");
                foreach (var item in availableSites)
                {
                    Console.WriteLine($"{item.SiteNumber, -15}  {item.MaxOccupancy, -15}  {item.Accessible, -15}  {item.MaxRVLength, -15}  {item.Utilities, -15}");
                }
                Console.WriteLine("What site should be reserved?");
                char tempSiteRes = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.WriteLine("What name should the reservation be made under?");
                string resName = Console.ReadLine();
                Console.ReadKey();
                bool wasSuccesful = resDal.InsertReservation(tempSiteRes, resName, arrivalDate, departureDate);
                if (wasSuccesful)
                {
                   resID =  resDal.GetReservationID(tempSiteRes, resName, arrivalDate, departureDate);
                    Console.WriteLine("Your reservation number is: " + resID);
                    Console.ReadKey();
                }
               
            }
        }

        public void SubSubMenu(Park park)
        {
            bool isDone = false;
            while (!isDone)
            {
                Console.Clear();
                Console.WriteLine("{0, -20}{1, -20}{2, -20}{3, -20}", "      Name", "      Open", "   Close", "  Daily Fee");
                PrintCampGrounds(park.Park_Id);
                Console.WriteLine();
                Console.WriteLine("Select a command:");
                Console.WriteLine();
                Console.WriteLine("   " + "1) Search for Available Reservation");
                Console.WriteLine("   " + "2) Return to Previous Screen");
                string uInput = Console.ReadLine();

                if (uInput == "2")
                {
                    isDone = true;
                }
                else if (uInput == "1")
                {
                    DateRangeSubMenu(park.Park_Id);


                    //GetSitesInCampground(camp);
                    //create another submenu for reservation
                    //reprint campground info in submenu
                }
            }

        }  
            
        
    }
}
