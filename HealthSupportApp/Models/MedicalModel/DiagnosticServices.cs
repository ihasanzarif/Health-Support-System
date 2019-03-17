using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.MedicalModel
{
    public class DiagnosticServices
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a diagnostic service")]
        public string ServiceName { get; set; }
        public int MedicalId { get; set; }
    }
}