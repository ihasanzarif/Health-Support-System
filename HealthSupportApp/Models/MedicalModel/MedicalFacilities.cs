using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.MedicalModel
{
    public class MedicalFacilities
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a hospital facility")]
        public string FacilityName { get; set; }
        public int MedicalId { get; set; }
    }
}