using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class SurveyHelper
    {
        /// <summary>
        /// List of SelectListItems for all Parks
        /// </summary>
        public static List<SelectListItem> ListsOfParks = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Cuyahoga Valley National Park", Value="CVNP" },
            new SelectListItem() { Text = "Everglades National Park", Value = "ENP" },
            new SelectListItem() { Text = "Grand Canyon National Park", Value = "GCNP" },
            new SelectListItem() { Text = "Glacier National Park", Value = "GNP" },
            new SelectListItem() { Text = "Great Smoky Mountains National Park", Value = "GSMNP" },
            new SelectListItem() { Text = "Grand Teton National Park", Value="GTNP" },
            new SelectListItem() { Text = "Mount Rainier National Park", Value = "MRNP" },
            new SelectListItem() { Text = "Rocky Mountain National Park", Value = "RMNP" },
            new SelectListItem() { Text = "Yellowstone National Park", Value = "YNP" },
            new SelectListItem() { Text = "Yosemite National Park", Value = "YNP2" }
        };
        /// <summary>
        /// List of SelectListItems for all States
        /// </summary>
        public static List<SelectListItem> ListsOfStates = new List<SelectListItem>()
        {
             new SelectListItem() { Text = "Alabama"},
             new SelectListItem() { Text = "Alaska"},
             new SelectListItem() { Text = "Arizona"},
             new SelectListItem() { Text = "Arkansas"},
             new SelectListItem() { Text = "California"},
             new SelectListItem() { Text = "Colorado", },
             new SelectListItem() { Text = "Connecticut", },
             new SelectListItem() { Text = "Delaware",},
             new SelectListItem() { Text = "Florida", },
             new SelectListItem() { Text = "Georgia", },
             new SelectListItem() { Text = "Hawaii", },
             new SelectListItem() { Text = "Idaho", },
             new SelectListItem() { Text = "Illinois", },
             new SelectListItem() { Text = "Indiana", },
             new SelectListItem() { Text = "Iowa", },
             new SelectListItem() { Text = "Kansas", },
             new SelectListItem() { Text = "Kentucky", },
             new SelectListItem() { Text = "Louisiana", },
             new SelectListItem() { Text = "Maine", },
             new SelectListItem() { Text = "Maryland", },
             new SelectListItem() { Text = "Massachusetts", },
             new SelectListItem() { Text = "Michigan", },
             new SelectListItem() { Text = "Minnesota", },
             new SelectListItem() { Text = "Mississippi", },
             new SelectListItem() { Text = "Missouri", },
             new SelectListItem() { Text = "Montana", },
             new SelectListItem() { Text = "Nebraska", },
             new SelectListItem() { Text = "Nevada", },
             new SelectListItem() { Text = "New Hampshire", },
             new SelectListItem() { Text = "New Jersey", },
             new SelectListItem() { Text = "New Mexico", },
             new SelectListItem() { Text = "New York", },
             new SelectListItem() { Text = "North Carolina", },
             new SelectListItem() { Text = "North Dakota", },
             new SelectListItem() { Text = "Ohio", },
             new SelectListItem() { Text = "Oklahoma", },
             new SelectListItem() { Text = "Oregon", },
             new SelectListItem() { Text = "Pennsylvania", },
             new SelectListItem() { Text = "Rhode Island", },
             new SelectListItem() { Text = "South Carolina", },
             new SelectListItem() { Text = "South Dakota", },
             new SelectListItem() { Text = "Tennessee", },
             new SelectListItem() { Text = "Texas", },
             new SelectListItem() { Text = "Utah", },
             new SelectListItem() { Text = "Vermont", },
             new SelectListItem() { Text = "Virginia", },
             new SelectListItem() { Text = "Washington", },
             new SelectListItem() { Text = "West Virginia", },
             new SelectListItem() { Text = "Wisconsin", },
             new SelectListItem() { Text = "Wyoming", },
        };
    }
}