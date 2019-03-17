using HealthSupportApp.Models.MedicalModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthSupportApp.Models.HomeModel
{
    public class SearchModel
    {
        [Required(ErrorMessage="* Please, Enter your Location")]
        [StringLength(50, ErrorMessage ="Maximum length exceeded.")]
        [AllowHtml]
        public string Location { get; set; }
        public bool SearchDoctor { get; set; }
        public bool SearchHospital { get; set; }
        public bool SearchBloodDonor { get; set; }
        [AllowHtml]
        [StringLength(50, ErrorMessage = "Maximum length exceeded.")]
        public string SearchString { get; set; }
        public MedicalAccount MedicalAccount { get; set; }
    }
}