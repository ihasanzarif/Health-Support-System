using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.MedicalModel
{
    public class Consultants
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a hospital consultant name")]
        public string ConsultantName { get; set; }
        public int MedicalId { get; set; }
    }
}