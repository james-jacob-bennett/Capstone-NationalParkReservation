using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class SurveyResult
    {
        public int SurveyId { get; set; }
        public string ParkCode { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please select your state")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please select the activity level")]
        public string ActivityLevel { get; set; }
    }
}